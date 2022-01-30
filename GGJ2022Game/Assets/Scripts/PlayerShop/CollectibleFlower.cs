using System;
using UnityEngine;

namespace Nidavellir.PlayerShop
{
    public class CollectibleFlower : MonoBehaviour
    {
        [SerializeField] private int m_worth;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                PlayerInventory.Instance.CurrencyAmount += this.m_worth;
                Destroy(this.gameObject);
            }
        }
    }
}