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

        public float RunInput => this.verticalAxisFactor * this.m_playerInput.actions["Run"].ReadValue<float>();

        public bool JumpTriggered => this.m_playerInput.actions["Jump"].triggered && this.m_playerInput.actions["Jump"].ReadValue<float>() > 0;
        public bool AttackTriggered => this.m_playerInput.actions["Attack"].triggered && this.m_playerInput.actions["Attack"].ReadValue<float>() > 0;
        public bool PushPullActivated => this.m_playerInput.actions["PushPull"].triggered && this.m_playerInput.actions["PushPull"].ReadValue<float>() > 0;
        public bool PushPullDeactivated => this.m_playerInput.actions["PushPull"].triggered && this.m_playerInput.actions["PushPull"].ReadValue<float>() <= 0;

        public bool RestartTriggered => this.m_playerInput.actions["Restart"].triggered;
        public bool QuitTriggered => this.m_playerInput.actions["QuitApplication"].triggered;

        private void Awake()
        {
            this.verticalAxisFactor = this.inverseVerticalAxis ? -1f : 1f;
        }
    }
}
