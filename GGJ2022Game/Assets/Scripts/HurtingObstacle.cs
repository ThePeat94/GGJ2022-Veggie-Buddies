using System;
using UnityEngine;

namespace Nidavellir
{
    public class HurtingObstacle : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent<PlayerController>(out var controller))
            {
                controller.Hurt();
            }
        }
    }
}