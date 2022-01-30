using System;
using UnityEngine;

namespace Nidavellir.PlayerShop
{
    public class KarlSkinLoader : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_eyes;
        [SerializeField] private MeshRenderer m_body;
        [SerializeField] private MeshRenderer m_pants;

        private void Start()
        {
            this.LoadSkin(PlayerInventory.Instance.ActiveKarlSkin);
        }

        private void LoadSkin(ShopItem toLoad)
        {
            if (toLoad == null)
                return;
            
            this.m_body.material = toLoad.BodyMaterial;
            this.m_eyes.material = toLoad.EyesMaterial;
            this.m_pants.material = toLoad.PantsMaterial;
        }
    }
}