using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nidavellir.PlayerShop.UI
{
    public class ShopCardUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_costText;
        [SerializeField] private TextMeshProUGUI m_nameText;
        [SerializeField] private Button m_activateButton;
        [SerializeField] private Button m_buyButton;
        [SerializeField] private Image m_image;

        private ShopItem m_data;
        private Shop m_shop;

        private AudioClip m_purchaseClip;

        public void CreateCard(ShopItem shopItem, Shop shop, AudioClip purchaseClip)
        {
            this.m_data = shopItem;
            this.m_shop = shop;
            this.m_purchaseClip = purchaseClip;
         
            this.m_costText.text = shopItem.Cost.ToString();
            this.m_nameText.text = shopItem.Name;
            this.m_image.sprite = shopItem.Image;

            this.UpdateUI();

            PlayerInventory.Instance.OnInventoryChanged += this.InventoryChanged;
        }

        private void InventoryChanged(object sender, System.EventArgs e)
        {
            this.UpdateUI();
        }

        private void UpdateUI()
        {
            var alreadyBought = PlayerInventory.Instance.BoughtShopItems.Contains(this.m_data);
            this.m_activateButton.gameObject.SetActive(alreadyBought);
            this.m_buyButton.gameObject.SetActive(!alreadyBought);

            if (this.m_data.PlayerType == PlayerType.FORWARD_PLAYER)
            {
                var isInactive = PlayerInventory.Instance.ActiveKarlSkin != this.m_data;
                this.m_activateButton.interactable = isInactive;
                this.m_activateButton.GetComponentInChildren<TextMeshProUGUI>()
                    .text = isInactive ? "Activate" : "Already active";
            }
            else if (this.m_data.PlayerType == PlayerType.BACKWARD_PLAYER)
            {
                var isInactive = PlayerInventory.Instance.ActiveGudrunSkin != this.m_data;
                this.m_activateButton.interactable = isInactive;
                this.m_activateButton.GetComponentInChildren<TextMeshProUGUI>()
                    .text = isInactive ? "Activate" : "Already active";
            }

            var canStillAfford = PlayerInventory.Instance.CurrencyAmount >= this.m_data.Cost;
            this.m_costText.color = canStillAfford ? Color.green : Color.red;
            this.m_buyButton.interactable = canStillAfford;


        }

        private void OnDestroy()
        {
            PlayerInventory.Instance.OnInventoryChanged -= this.InventoryChanged;
        }

        public void BuyShopItem()
        {
            if (this.m_shop.TryBuyItem(this.m_data))
            {
                AudioSource.PlayClipAtPoint(this.m_purchaseClip, Camera.main.transform.position);
                this.m_activateButton.gameObject.SetActive(true);
                this.m_buyButton.gameObject.SetActive(false);
            }
        }

        public void ActivateShopItem()
        {
            if (this.m_data.PlayerType == PlayerType.FORWARD_PLAYER)
                PlayerInventory.Instance.ActiveKarlSkin = this.m_data;
            else if (this.m_data.PlayerType == PlayerType.BACKWARD_PLAYER)
                PlayerInventory.Instance.ActiveGudrunSkin = this.m_data;
        }
    }
}