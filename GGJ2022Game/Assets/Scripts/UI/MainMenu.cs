using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject m_startMenu;
        [SerializeField] private GameObject m_credits;
        [SerializeField] private AudioClip m_gameTheme;
        
        public void StartGame()
        {
            MusicPlayer.Instance.PlayLoopingMusic(this.m_gameTheme);
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

        public void OpenLink(string url)
        {
            Application.OpenURL(url);
        }
    }
}
