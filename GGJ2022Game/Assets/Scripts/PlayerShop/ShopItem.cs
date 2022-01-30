using System;
using UnityEngine;

namespace Nidavellir.PlayerShop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Data/ShopItem", order = 0)]
    public class ShopItem : ScriptableObject
    {
        [SerializeField] private PlayerType m_playerType;
        [SerializeField] private int m_currencyCost;
        [SerializeField] private int m_id;
        [SerializeField] private GameObject m_gameObjectToLoad;
        [SerializeField] private string m_name;
        [SerializeField] private Sprite m_image;

        public int Id => this.m_id;
        public int Cost => this.m_currencyCost;
        public PlayerType PlayerType => this.m_playerType;
        public GameObject GameObjectToLoad => this.m_gameObjectToLoad;
        public string Name => this.m_name;
        public Sprite Image => this.m_image;
    }
}