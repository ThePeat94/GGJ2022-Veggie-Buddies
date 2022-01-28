﻿using UnityEngine;

namespace Nidavellir
{
    public class InputProcessor : MonoBehaviour
    {
        private PlayerInput m_playerInput;
        private Vector2 m_movementInput;

        public Vector2 Movement => this.m_movementInput;
        public bool InteractTriggered => this.m_playerInput.Actions.Interact.triggered;
        public bool InspectTriggered => this.m_playerInput.Actions.Inspect.triggered;
        public bool RestartTriggered => this.m_playerInput.Actions.Restart.triggered;
    
        private void Awake()
        {
            this.m_playerInput = new PlayerInput();
        }

        private void OnEnable()
        {
            this.m_playerInput?.Enable();
        }

        private void Update()
        {
            this.m_movementInput = this.m_playerInput.Actions.Move.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            this.m_playerInput?.Disable();
            this.m_movementInput = Vector3.zero;
        }
    }
}
