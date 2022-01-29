using System;
using UnityEngine;

namespace Nidavellir
{
    public class HurtingObstacle : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"Collided with: {other.gameObject}");
            if (other.gameObject.TryGetComponent<PlayerController>(out var controller))
            {
                controller.PlayerHurt();
            }
        }
    }
}