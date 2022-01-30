using UnityEngine;

namespace Nidavellir
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class PushPullable : MonoBehaviour
    {
        private readonly float m_skinWidth = .1f;

        [SerializeField] private LayerMask m_layerMask;

        private Rigidbody m_rigidbody;
        private BoxCollider m_boxCollider;

        private void Awake()
        {
            this.m_rigidbody = GetComponent<Rigidbody>();
            this.m_boxCollider = GetComponent<BoxCollider>();
        }

        public bool TrySetTargetPosition(Vector3 position)
        {
            var delta = position - this.transform.position;
            var distance = delta.magnitude;
            var direction = delta / distance;

            var origin = this.transform.position + this.transform.TransformDirection(Vector3.right * (this.m_boxCollider.size.x - this.m_skinWidth));
            if (Physics.Raycast(origin, direction, out var hitHinfo, distance * 2f, this.m_layerMask))
            {
                if ((hitHinfo.distance - this.m_skinWidth) <= distance)
                {
                    this.m_rigidbody.position = this.m_rigidbody.position + this.transform.TransformDirection(Vector3.right * (hitHinfo.distance - this.m_skinWidth));
                    return false;
                }
            }
            this.m_rigidbody.position = position;
            return true;
        }
    }
}