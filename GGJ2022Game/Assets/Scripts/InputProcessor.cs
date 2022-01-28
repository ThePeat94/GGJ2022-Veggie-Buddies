using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Nidavellir
{
    public class InputProcessor : MonoBehaviour
    {
        [SerializeField]
        private bool inverseVerticalAxis;

        private float verticalAxisFactor = 1f;

        public float RunInput { get; private set; }

        public UnityEvent JumpTriggered { get; set; } = new UnityEvent();

        private void Awake()
        {
            this.verticalAxisFactor = this.inverseVerticalAxis ? -1f : 1f;
        }

        public void OnRunInputChanged(InputAction.CallbackContext value)
        {
            this.RunInput = this.verticalAxisFactor * value.ReadValue<float>();
        }

        public void OnJumpTriggered(InputAction.CallbackContext value)
        {
            if (value.ReadValueAsButton())
                this.JumpTriggered.Invoke();
        }
    }
}
