using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nidavellir.EventArgs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    public class GameStateManager : MonoBehaviour
    {
        private static GameStateManager s_instance;

        [SerializeField] private InputProcessor m_inputProcessor;

        private bool m_anyPlayerDied;
        private bool m_levelHasSucceeded;
        private bool m_forwardPlayerReachedGoal;
        private bool m_backwardPlayerReachedGoal;
        
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
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
            
            SceneManager.sceneLoaded += this.OnSceneLoaded;
            
#if UNITY_EDITOR
            // TODO: Remove when setting up scenes properly
            this.RegisterPlayerEvents();
            this.RegisterPlayerGoals();
#endif
        }

        private void RegisterPlayerGoals()
        {
            var goals = FindObjectsOfType<PlayerGoal>();
            var forwardPlayerGoal = goals.FirstOrDefault(g => g.GoalForPlayerType == PlayerType.FORWARD_PLAYER);
            var backwardPlayerGoal = goals.FirstOrDefault(g => g.GoalForPlayerType == PlayerType.BACKWARD_PLAYER);
            
            forwardPlayerGoal.OnPlayerReachedGoal += this.ForwardPlayerReachedGoal;
            backwardPlayerGoal.OnPlayerReachedGoal += this.BackwardPlayerReachedGoal;
        }

        private void BackwardPlayerReachedGoal(object sender, System.EventArgs e)
        {
            this.m_backwardPlayerReachedGoal = true;
            this.CheckGameSuccess();
        }

        private void ForwardPlayerReachedGoal(object sender, System.EventArgs e)
        {
            this.m_forwardPlayerReachedGoal = true;
            this.CheckGameSuccess();
        }

        private void Update()
        {
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
            this.m_anyPlayerDied = false;
            this.m_forwardPlayerReachedGoal = false;
            this.m_backwardPlayerReachedGoal = false;
            
            this.RegisterPlayerEvents();
            
            // TODO: Load Targets and subscribe to goal reached
        }

        private void RegisterPlayerEvents()
        {
            var playerControllers = FindObjectsOfType<PlayerController>();
            foreach (var playerController in playerControllers)
            {
                playerController.OnPlayerDied += this.AnyPlayerDied;
            }
        }

        private void AnyPlayerDied(object sender, System.EventArgs e)
        {
            this.m_anyPlayerDied = true;
            this.m_gameOver?.Invoke(this, System.EventArgs.Empty);
        }

        private void CheckGameSuccess()
        {
            if (this.m_forwardPlayerReachedGoal && this.m_backwardPlayerReachedGoal)
            {
                this.m_levelHasSucceeded = true;
                this.m_levelSucceeded?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}
