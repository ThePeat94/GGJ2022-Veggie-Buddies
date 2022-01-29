using UnityEngine;

namespace Nidavellir
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        private void Awake()
        {
            this.m_rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 velocity)
        {
            this.m_rigidbody.velocity = velocity;
        }
    }
}