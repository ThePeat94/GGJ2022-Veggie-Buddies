using UnityEngine;

namespace Nidavellir.UI
{
    public class GameSuccessUI : MonoBehaviour
    {
        
        [SerializeField] private GameObject m_gameOverScreen;
        [SerializeField] private GameObject m_levelSucceededScreen;
        
        public void ShowLevelSucceededScreen()
        {
            this.m_levelSucceededScreen.SetActive(true);
        }

        public void ShowGameOverScreen()
        {
            this.m_gameOverScreen.SetActive(true);
        }    
        
        public void HideLevelSucceededScreen()
        {
            this.m_levelSucceededScreen.SetActive(false);
        }

        public void HideGameOverScreen()
        {
            this.m_gameOverScreen.SetActive(false);
        }
    }
}