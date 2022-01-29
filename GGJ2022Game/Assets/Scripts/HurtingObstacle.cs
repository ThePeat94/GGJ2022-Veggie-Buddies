using System;
using UnityEngine;

namespace Nidavellir
{
    public class HurtingObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"HurtingObstacle collided with: {other.gameObject}");
            var controller = other.gameObject.GetComponentInParent<PlayerController>();
            if (controller != null)
            {
                controller.PlayerHurt();
            }
        }
    }
}