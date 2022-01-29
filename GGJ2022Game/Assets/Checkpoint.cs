using System;
using Nidavellir.EventArgs;
using UnityEngine;

namespace Nidavellir
{
    public class Checkpoint : MonoBehaviour
    {
        private EventHandler<PlayerTypeEventArgs> m_playerReachedCheckpoint;

        [SerializeField] private AudioClip m_checkpointPassedClip;
        [SerializeField] private Transform m_spawnPoint;

        public event EventHandler<PlayerTypeEventArgs> OnPlayerReachedCheckpoint
        {
            add => this.m_playerReachedCheckpoint += value;
            remove => this.m_playerReachedCheckpoint -= value;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                Debug.Log($"Payer {playerController.PlayerType} reached checkpoint");
                FindObjectOfType<OneShotSfxPlayer>().PlayOneShot(this.m_checkpointPassedClip);
                this.m_playerReachedCheckpoint?.Invoke(this, new PlayerTypeEventArgs(playerController.PlayerType, this.m_spawnPoint.position));
            }
        }
    }
}