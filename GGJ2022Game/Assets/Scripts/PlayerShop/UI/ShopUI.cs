using System;
using UnityEngine;

namespace Nidavellir.PlayerShop.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_shopCardPrefab;
        [SerializeField] private GameObject m_cardsParent;

        [SerializeField] private Shop m_shop;
        
        private void Start()
        {
            this.CreateCardsForAvailableItems();
        }

        private void CreateCardsForAvailableItems()
        {
            foreach (var shopItem in this.m_shop.AvailableItems)
            {
                var createdCard = Instantiate(this.m_shopCardPrefab, this.m_cardsParent.transform)
                    .GetComponent<ShopCardUI>();
                createdCard.CreateCard(shopItem, this.m_shop);
            }
        }
    }
}