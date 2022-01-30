using UnityEngine;

namespace Nidavellir
{
    public class MainThemePlayer : MonoBehaviour
    {
        private GameStateManager m_gameStateManager;
        private AudioSource m_audioSource;

        // Start is called before the first frame update
        void Start()
        {
            this.m_audioSource = GetComponent<AudioSource>();
            this.m_gameStateManager = FindObjectOfType<GameStateManager>();
            this.m_gameStateManager.OnGameOver += OnGameOver;
        }

        private void OnGameOver(object sender, System.EventArgs e)
        {
            this.m_audioSource.Stop();
        }
    }
}
