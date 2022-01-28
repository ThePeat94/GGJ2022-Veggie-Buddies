using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;

        private CharacterController m_characterController;
        private InputProcessor m_inputProcessor;
        private Animator m_animator;

        private readonly int m_isWalkingHash = Animator.StringToHash("IsWalking");

        private bool m_jumpTriggered = false;

        private float m_locomotionVelocity = 0f;
        private float m_jumpVelocity = 0f;


        public void Hurt()
        {
            Debug.Log("Player got hurt.");
        }

        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_characterController = this.GetComponent<CharacterController>();
            this.m_animator = this.GetComponent<Animator>();
        }

        private void Start()
        {
            this.m_inputProcessor.JumpTriggered.AddListener(this.TriggerJump);
        }

        private void TriggerJump()
        {
            this.m_jumpTriggered = true;
        }

        private void FixedUpdate()
        {
            //var positionAtStartOfFixedUpdate = this.transform.position;

            this.ApplyGravity(Time.fixedDeltaTime); // we have to apply gravity first to make sure the CharacterController.isGrounded property works
            this.ApplyLocomotion(Time.fixedDeltaTime);
            this.UpdateLookDirection();

            //this.m_jumpVelocity = (this.transform.position - positionAtStartOfFixedUpdate) / Time.fixedDeltaTime;
        }

        private void LateUpdate()
        {
            this.UpdateAnimator();
        }

        private void ApplyLocomotion(float deltaTime)
        {
            var isGrounded = this.m_characterController.isGrounded;

            this.m_locomotionVelocity = this.m_inputProcessor.RunInput * this.m_playerData.MovementSpeed;
            if (Mathf.Abs(this.m_locomotionVelocity) > Mathf.Epsilon)
                this.m_characterController.Move(Vector3.right * deltaTime * this.m_locomotionVelocity);

            if (this.m_jumpTriggered)
            {
                this.m_jumpTriggered = false;
                if (isGrounded)
                {
                    this.m_jumpVelocity = this.m_playerData.JumpVelocity;
                    //this.m_characterController.SimpleMove(Vector3.up * this.m_playerData.JumpHeight);
                }
            }
        }

        private void ApplyGravity(float deltaTime)
        {
            this.m_jumpVelocity += deltaTime * Physics.gravity.y;
            if (this.m_jumpVelocity < 0)
                this.m_jumpVelocity = 0;
            this.m_characterController.Move(deltaTime * (Physics.gravity + Vector3.up * this.m_jumpVelocity));
        }

        private void UpdateLookDirection()
        {
            if (Mathf.Abs(this.m_locomotionVelocity) > Mathf.Epsilon)
                this.m_characterController.transform.rotation = Quaternion.LookRotation(Vector3.right * Mathf.Sign(this.m_locomotionVelocity), Vector3.up);
        }

        private void UpdateAnimator()
        {
            this.m_animator.SetBool(m_isWalkingHash, Mathf.Abs(this.m_locomotionVelocity) > Mathf.Epsilon);
        }
    }
}