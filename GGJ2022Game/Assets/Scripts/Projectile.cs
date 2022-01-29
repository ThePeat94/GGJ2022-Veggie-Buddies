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

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }

        public void Launch(Vector3 force)
        {
            this.m_rigidbody.AddForce(force);
        }
    }
}