using System;
using UnityEngine;

namespace Nidavellir.PlayerShop
{
    public class GudrunSkinLoader : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_eyes;
        [SerializeField] private MeshRenderer m_body;

        private void Start()
        {
            this.LoadSkin(PlayerInventory.Instance.ActiveGudrunSkin);
        }

        private void LoadSkin(ShopItem toLoad)
        {
            if (toLoad == null)
                return;
            
            this.m_eyes.material = toLoad.EyesMaterial;

            var materials = this.m_body.materials;
            materials[0] = toLoad.BodyMaterial;
            materials[1] = toLoad.PantsMaterial;

            this.m_body.materials = materials;
        }
    }
}