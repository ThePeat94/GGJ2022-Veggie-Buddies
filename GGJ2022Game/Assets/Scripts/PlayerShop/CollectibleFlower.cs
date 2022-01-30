using System;
using UnityEngine;

namespace Nidavellir.PlayerShop
{
    public class CollectibleFlower : MonoBehaviour
    {
        [SerializeField] private int m_worth;
        [SerializeField] private AudioClip m_collectSound;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                PlayerInventory.Instance.CurrencyAmount += this.m_worth;
                FindObjectOfType<OneShotSfxPlayer>()
                    .PlayOneShot(this.m_collectSound);
                Destroy(this.gameObject);
            }
        }
    }
}