using System;
using UnityEngine;

namespace Nidavellir
{
    public class DeadZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                playerController.KillPlayer();
            }
        }
    }
}