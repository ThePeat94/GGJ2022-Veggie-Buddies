using UnityEngine;

namespace Nidavellir
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float m_rotationSpeed;

        void Update()
        {
            transform.Rotate(Vector3.right, m_rotationSpeed, Space.World);
        }
    }
}
