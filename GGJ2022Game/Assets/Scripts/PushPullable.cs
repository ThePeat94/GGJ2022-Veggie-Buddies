using UnityEngine;

namespace Nidavellir
{
    [RequireComponent(typeof(Rigidbody))]
    public class PushPullable : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        private void Awake()
        {
            this.m_rigidbody = GetComponent<Rigidbody>();
        }

        public void SetPosition(Vector3 position)
        {
            this.m_rigidbody.position = position;
        }
    }
}