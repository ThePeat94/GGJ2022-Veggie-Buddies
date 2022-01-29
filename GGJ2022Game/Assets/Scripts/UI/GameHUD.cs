﻿using System;
using UnityEngine;

namespace Nidavellir.UI
{
    public class GameHUD : MonoBehaviour
    {
        private static GameHUD s_instance;
        private GameSuccessUI m_gameSuccessUi;
        private PumpkinSeedsDisplay m_pumpkinSeedDisplay;

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

            this.m_gameSuccessUi = this.GetComponentInChildren<GameSuccessUI>();
            this.m_pumpkinSeedDisplay = this.GetComponentInChildren<PumpkinSeedsDisplay>();
        }

        private void Start()
        {
            GameStateManager.Instance.OnGameOver += this.OnGameOver;
            GameStateManager.Instance.OnLevelSucceeded += this.OnLevelSucceeded;
        }

        private void OnLevelSucceeded(object sender, System.EventArgs e)
        {
            this.m_gameSuccessUi.ShowLevelSucceededScreen();
        }

        private void OnGameOver(object sender, System.EventArgs e)
        {
            this.m_gameSuccessUi.ShowGameOverScreen();
        }

        internal void SetPumpkinSeedCount(int v)
        {
            this.m_pumpkinSeedDisplay.SetPumpkinSeedCount(v);
        }
    }
}