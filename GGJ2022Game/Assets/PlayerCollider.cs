using UnityEngine;

namespace Nidavellir
{
    public class PlayerCollider : MonoBehaviour
    {
        [SerializeField] PlayerController m_playerController;

        public PlayerController PlayerController => this.m_playerController;
    }
}
