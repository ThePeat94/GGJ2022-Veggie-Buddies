using System;
using System.Collections.Generic;

namespace Nidavellir.PlayerShop
{
    public class PlayerInventory
    {
        private static PlayerInventory s_instance;
        public static PlayerInventory Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = new();

                return s_instance;
            }
        }

        private int m_currencyAmount;
        private ShopItem m_activeKarlItem;
        private ShopItem m_actveGudrunItem;

        private EventHandler m_inventoryChanged;

        private List<ShopItem> m_boughtShopItems;

        private PlayerInventory()
        {
            this.m_boughtShopItems = new();
        }
        
        public event EventHandler OnInventoryChanged
        {
            add => this.m_inventoryChanged += value;
            remove => this.m_inventoryChanged -= value;
        }

        public void AddShopItem(ShopItem shopItem)
        {
            this.m_boughtShopItems.Add(shopItem);
            this.m_inventoryChanged?.Invoke(this, System.EventArgs.Empty);
        }
        
        
        public IReadOnlyList<ShopItem> BoughtShopItems => this.m_boughtShopItems;

        public int CurrencyAmount
        {
            get => this.m_currencyAmount;
            set
            {
                this.m_currencyAmount = value;
                this.m_inventoryChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }

        public ShopItem ActiveKarlSkin
        {
            get => this.m_activeKarlItem;
            set
            {
                this.m_activeKarlItem = value;
                this.m_inventoryChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }
        public ShopItem ActiveGudrunSkin
        {
            get => this.m_actveGudrunItem;
            set
            {
                this.m_actveGudrunItem = value;
                this.m_inventoryChanged?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}