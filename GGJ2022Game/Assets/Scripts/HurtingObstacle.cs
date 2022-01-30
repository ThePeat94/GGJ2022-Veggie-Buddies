using System;
using UnityEngine;

namespace Nidavellir
{
    public class HurtingObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"HurtingObstacle triggered by: {other.gameObject}");
            var collider = other.gameObject.GetComponent<PlayerCollider>();
            if (collider != null)
            {
                collider.PlayerController.PlayerHurt();
            }
        }
    }
}