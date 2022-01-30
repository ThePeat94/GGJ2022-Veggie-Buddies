using System;
using TMPro;
using UnityEngine;

namespace Nidavellir.PlayerShop.UI
{
    public class FlowerCountUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;
        
        private void Start()
        {
            PlayerInventory.Instance.OnInventoryChanged += this.InventoryChanged;
            this.UpdateUI();
        }

        private void InventoryChanged(object sender, System.EventArgs e)
        {
            this.UpdateUI();
        }
        
        private void OnDestroy()
        {
            PlayerInventory.Instance.OnInventoryChanged -= this.InventoryChanged;
        }

        private void UpdateUI()
        {
            this.m_text.text = PlayerInventory.Instance.CurrencyAmount.ToString();
        }
    }
}