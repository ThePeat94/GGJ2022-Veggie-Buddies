using System;
using UnityEngine;

namespace Nidavellir.UI
{
    public class GameHUD : MonoBehaviour
    {
        private static GameHUD s_instance;

        [SerializeField] private GameObject m_gameOverScreen;
        

        public static GameHUD Instance => s_instance;
        
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
        }

        private void Start()
        {
            GameStateManager.Instance.OnGameOver += this.OnGameOver;
        }

        private void OnGameOver(object sender, System.EventArgs e)
        {
            this.ShowGameOverScreen();
        }

        private void ShowGameOverScreen()
        {
            this.m_gameOverScreen.SetActive(true);
        }
    }
}