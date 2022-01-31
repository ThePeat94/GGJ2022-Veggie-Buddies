using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_startMenu;
        [SerializeField] private GameObject m_credits;
        [SerializeField] private GameObject m_shopMenu;

        [SerializeField] private AudioClip m_shopEnterSound;
        [SerializeField] private AudioClip m_shopLeftSound;

        [SerializeField] private AudioSource m_shopAudioSource;

        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }

        public void ShowCredits()
        {
            this.m_startMenu.SetActive(false);
            this.m_credits.SetActive(true);
        }

        public void BackFromCreditsToStart()
        {
            this.m_startMenu.SetActive(true);
            this.m_credits.SetActive(false);
        }

        public void ShowShop()
        {
            this.m_shopAudioSource.clip = this.m_shopEnterSound;
            this.m_shopAudioSource.Play();
            this.m_startMenu.SetActive(false);
            this.m_shopMenu.SetActive(true);
        }
        
        public void BackFromShopToStart()
        {
            this.m_shopAudioSource.clip = this.m_shopLeftSound;
            this.m_shopAudioSource.Play();
            this.m_startMenu.SetActive(true);
            this.m_shopMenu.SetActive(false);
        }

        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }
    }
}
