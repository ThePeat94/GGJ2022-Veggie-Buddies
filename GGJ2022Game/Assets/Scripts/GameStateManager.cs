using System;
using System.Collections.Generic;
using System.Linq;
using Nidavellir.EventArgs;
using Nidavellir.PlayerShop;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class GameStateManager : MonoBehaviour
    {
        private static GameStateManager s_instance;

        [SerializeField] private InputProcessor m_inputProcessor;
        [SerializeField] private ShopItem m_defaultKarlSkin;
        [SerializeField] private ShopItem m_defaultGudrunSkin;
        [SerializeField] private AudioClip m_karlReachedGoal;
        [SerializeField] private AudioClip m_gudrunReachedGoal;
        [SerializeField] private AudioClip m_levelSucceededSfx;

        private bool m_anyPlayerDied;
        private bool m_levelHasSucceeded;
        private bool m_forwardPlayerReachedGoal;
        private bool m_backwardPlayerReachedGoal;

        private PlayerController m_forwardPlayer;
        private PlayerController m_backwardPlayer;

        private Dictionary<PlayerType, Vector3?> m_latestCheckpointPassedPerPlayer = new()
        {
            [PlayerType.FORWARD_PLAYER] = null,
            [PlayerType.BACKWARD_PLAYER] = null
        };

        private EventHandler m_gameOver;
        private EventHandler m_levelSucceeded;

        public static GameStateManager Instance => s_instance;

        public event EventHandler OnGameOver
        {
            add => this.m_gameOver += value;
            remove => this.m_gameOver -= value;
        }
        
        public event EventHandler OnLevelSucceeded
        {
            add => this.m_levelSucceeded += value;
            remove => this.m_levelSucceeded -= value;
        }

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
            
            // Default Skins
            PlayerInventory.Instance.AddShopItem(this.m_defaultKarlSkin);
            PlayerInventory.Instance.AddShopItem(this.m_defaultGudrunSkin);
            PlayerInventory.Instance.ActiveKarlSkin = this.m_defaultKarlSkin;
            PlayerInventory.Instance.ActiveGudrunSkin = this.m_defaultGudrunSkin;
        }

        private void Start()
        {
            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }

        private void RegisterPlayerCheckPoints()
        {
            var checkPoints = FindObjectsOfType<Checkpoint>();
            foreach (var checkPoint in checkPoints)
            {
                checkPoint.OnPlayerReachedCheckpoint += this.OnyAnyPlayerReachedCheckpoint;
            }
        }

        private void OnyAnyPlayerReachedCheckpoint(object sender, PlayerTypeEventArgs e)
        {
            Debug.Log($"Setting Respawn for {e.AffectedPlayerType} to {e.RespawnPoint}");
            this.m_latestCheckpointPassedPerPlayer[e.AffectedPlayerType] = e.RespawnPoint;
        }

        private void RegisterPlayerGoals()
        {
            var goals = FindObjectsOfType<PlayerGoal>();
            var forwardPlayerGoal = goals.First(g => g.GoalForPlayerType == PlayerType.FORWARD_PLAYER);
            var backwardPlayerGoal = goals.First(g => g.GoalForPlayerType == PlayerType.BACKWARD_PLAYER);
            
            forwardPlayerGoal.OnPlayerReachedGoal += this.ForwardPlayerReachedGoal;
            backwardPlayerGoal.OnPlayerReachedGoal += this.BackwardPlayerReachedGoal;
        }

        private void BackwardPlayerReachedGoal(object sender, System.EventArgs e)
        {
            this.m_backwardPlayerReachedGoal = true;
            
            if(!this.m_forwardPlayerReachedGoal)
                FindObjectOfType<OneShotSfxPlayer>()?.PlayOneShot(this.m_gudrunReachedGoal);
            this.CheckGameSuccess();
        }

        private void ForwardPlayerReachedGoal(object sender, System.EventArgs e)
        {
            this.m_forwardPlayerReachedGoal = true;
            
            if(!this.m_backwardPlayerReachedGoal)
                FindObjectOfType<OneShotSfxPlayer>()?.PlayOneShot(this.m_karlReachedGoal);
            
            this.CheckGameSuccess();
        }

        private void Update()
        {
            if(this.m_inputProcessor.QuitTriggered)
            {
                Application.Quit();
                return;
            }

            var shouldGoBackToMainMenu = (this.m_anyPlayerDied || this.m_levelHasSucceeded) && this.m_inputProcessor.BackToMenuTriggered;
            if (shouldGoBackToMainMenu)
            {
                SceneManager.LoadScene(0);
                return;
            }

            var shouldRestart = this.m_anyPlayerDied && this.m_inputProcessor.RestartTriggered;
            if (shouldRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            
            var shouldContinue = this.m_levelHasSucceeded && this.m_inputProcessor.RestartTriggered;
            if (shouldContinue)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 0)
                return;
            
            
            this.m_anyPlayerDied = false;
            this.m_forwardPlayerReachedGoal = false;
            this.m_backwardPlayerReachedGoal = false;
            this.m_levelHasSucceeded = false;
            
            var players = FindObjectsOfType<PlayerController>();
            this.m_latestCheckpointPassedPerPlayer[PlayerType.FORWARD_PLAYER] = null;
            this.m_latestCheckpointPassedPerPlayer[PlayerType.BACKWARD_PLAYER] = null;
            this.m_forwardPlayer = players.First(pc => pc.PlayerType == PlayerType.FORWARD_PLAYER);
            this.m_backwardPlayer = players.First(pc => pc.PlayerType == PlayerType.BACKWARD_PLAYER);
            
            this.RegisterPlayerEvents();
            this.RegisterPlayerGoals();
            this.RegisterPlayerCheckPoints();

            this.RespawnPlayer(this.m_forwardPlayer);
            this.RespawnPlayer(this.m_backwardPlayer);
            LevelTimer.Instance.RestartStopWatch();
        }

        private void RespawnPlayer(PlayerController toRespawn)
        {
            if (this.m_latestCheckpointPassedPerPlayer[toRespawn.PlayerType].HasValue)
            {
                toRespawn.RespawnPlayer(this.m_latestCheckpointPassedPerPlayer[toRespawn.PlayerType].Value);
            }
        }

        private void RegisterPlayerEvents()
        {
            this.m_forwardPlayer.OnPlayerDied += this.KarlPlayerDied;
            this.m_backwardPlayer.OnPlayerDied += this.GudrunPlayerDied;
        }

        private void KarlPlayerDied(object sender, System.EventArgs e)
        {
            this.m_anyPlayerDied = true;
            this.m_backwardPlayer.PreventMovement();
            this.m_gameOver?.Invoke(this, System.EventArgs.Empty);
        }        
        
        private void GudrunPlayerDied(object sender, System.EventArgs e)
        {
            this.m_anyPlayerDied = true;
            this.m_forwardPlayer.PreventMovement();
            this.m_gameOver?.Invoke(this, System.EventArgs.Empty);
        }

        private void CheckGameSuccess()
        {
            if (this.m_forwardPlayerReachedGoal && this.m_backwardPlayerReachedGoal)
            {
                this.m_levelHasSucceeded = true;
                this.m_latestCheckpointPassedPerPlayer[PlayerType.FORWARD_PLAYER] = null;
                this.m_latestCheckpointPassedPerPlayer[PlayerType.BACKWARD_PLAYER] = null;
                LevelTimer.Instance.StopStopWatch();
                FindObjectOfType<OneShotSfxPlayer>()?.PlayOneShot(this.m_levelSucceededSfx);
                this.m_levelSucceeded?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
