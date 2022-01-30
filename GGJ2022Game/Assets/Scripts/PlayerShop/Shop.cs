using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir.PlayerShop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<ShopItem> m_availableItems;

        public IReadOnlyList<ShopItem> AvailableItems => this.m_availableItems;

        public bool TryBuyItem(ShopItem shopItem)
        {
            if (shopItem.Cost > PlayerInventory.Instance.CurrencyAmount)
                return false;

            PlayerInventory.Instance.CurrencyAmount -= shopItem.Cost;
            PlayerInventory.Instance.AddShopItem(shopItem);
            return true;
        }
    }
}