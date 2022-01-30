using System;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerGoal : MonoBehaviour
    {
        [SerializeField] private PlayerType m_goalForPlayerType;

        [SerializeField] private AudioClip m_karlReachedGoal;
        [SerializeField] private AudioClip m_gudrunReachedGoal;
        
        private EventHandler m_playerReachedGoal;
        
        public PlayerType GoalForPlayerType => this.m_goalForPlayerType;
        public event EventHandler OnPlayerReachedGoal
        {
            add => this.m_playerReachedGoal += value;
            remove => this.m_playerReachedGoal -= value;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out var playerController) && playerController.PlayerType == this.m_goalForPlayerType)
            {
                Debug.Log($"{playerController.PlayerType} reached goal");
                playerController.PreventMovement();
                
                if(playerController.PlayerType == PlayerType.FORWARD_PLAYER)
                    FindObjectOfType<OneShotSfxPlayer>().PlayOneShot(this.m_karlReachedGoal);
                else
                    FindObjectOfType<OneShotSfxPlayer>().PlayOneShot(this.m_gudrunReachedGoal);
                

                
                this.m_playerReachedGoal?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}