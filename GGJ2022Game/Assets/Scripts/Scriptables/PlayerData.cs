using UnityEngine;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float m_movementSpeed;
        [SerializeField] private float m_rotationSpeed;
        [SerializeField] private float m_jumpForce;

        public float MovementSpeed => this.m_movementSpeed;
        public float RotationSpeed => this.m_rotationSpeed;
        public float JumpForce => this.m_jumpForce;
    }
}