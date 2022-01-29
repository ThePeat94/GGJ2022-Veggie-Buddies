using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Nidavellir
{
    public class InputProcessor : MonoBehaviour
    {
        [SerializeField] public PlayerInput m_playerInput;

        [SerializeField] private bool inverseVerticalAxis;

        private float verticalAxisFactor = 1f;

        public float RunInput { get; private set; }

        public bool JumpTriggered { get; set; }

        public bool RestartTriggered =>
            this.m_playerInput.actions["Restart"]
                .triggered;

        private void Awake()
        {
            this.verticalAxisFactor = this.inverseVerticalAxis ? -1f : 1f;
        }

        private void Update()
        {
            this.RunInput = this.verticalAxisFactor * this.m_playerInput.actions["Run"].ReadValue<float>();
            this.JumpTriggered = this.m_playerInput.actions["Jump"].triggered && this.m_playerInput.actions["Jump"].ReadValue<float>() > 0;
        }
    }
}
