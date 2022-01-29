using System;
using UnityEngine;

namespace Nidavellir.UI
{
    public class GameHUD : MonoBehaviour
    {
        private static GameHUD s_instance;
        private GameSuccessUI m_gameSuccessUi;

        private void Awake()
        {
            this.m_gameSuccessUi = this.GetComponentInChildren<GameSuccessUI>();
        }

        private void Start()
        {
            GameStateManager.Instance.OnGameOver += this.OnGameOver;
            GameStateManager.Instance.OnLevelSucceeded += this.OnLevelSucceeded;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameOver -= this.OnGameOver;
            GameStateManager.Instance.OnLevelSucceeded -= this.OnLevelSucceeded;
        }

        private void OnLevelSucceeded(object sender, System.EventArgs e)
        {
            this.m_gameSuccessUi.ShowLevelSucceededScreen();
        }

        private void OnGameOver(object sender, System.EventArgs e)
        {
            this.m_gameSuccessUi.ShowGameOverScreen();
        }
    }
}