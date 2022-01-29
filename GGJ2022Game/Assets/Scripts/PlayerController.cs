using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;

        [SerializeField] private AudioClip m_runningLoopAudioClip;
        [SerializeField] private AudioClip m_jumpAudioClip;
        [SerializeField] private AudioClip m_landAudioClip;

        private CharacterController m_characterController;
        private InputProcessor m_inputProcessor;
        private Animator m_animator;
        private AudioSource m_audioSource;

        private readonly int m_isWalkingHash = Animator.StringToHash("IsWalking");

        private bool m_jumpTriggered = false;

        private float m_locomotionVelocity = 0f;
        private bool m_isLocomoting;
        private float m_jumpVelocity = 0f;
        private bool m_hasJumpVelocity = false;
        private bool m_isGrounded = true;

        public void Hurt()
        {
            Debug.Log("Player got hurt.");
        }

        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_characterController = this.GetComponent<CharacterController>();
            this.m_animator = this.GetComponent<Animator>();
            this.m_audioSource = this.GetComponent<AudioSource>();
        }

        private void Update()
        {
            // we must read if a jump was triggered Update() although we need it in FixedUpdate() because we otherwise occasionally miss button presses
            if (this.m_inputProcessor.JumpTriggered)
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
            var wasGrounded = this.m_isGrounded;
            this.m_isGrounded = this.m_characterController.isGrounded;

            if (!wasGrounded && this.m_isGrounded)
                this.m_audioSource.PlayOneShot(this.m_landAudioClip);

            this.m_locomotionVelocity = this.m_inputProcessor.RunInput * this.m_playerData.MovementSpeed;
            this.m_isLocomoting = Mathf.Abs(this.m_locomotionVelocity) > Mathf.Epsilon;

            if (this.m_isLocomoting)
            {
                this.m_characterController.Move(Vector3.right * deltaTime * this.m_locomotionVelocity);
            }

            if (this.m_isGrounded && this.m_isLocomoting)
            {
                if (!this.m_audioSource.isPlaying)
                {
                    this.m_audioSource.clip = m_runningLoopAudioClip;
                    this.m_audioSource.loop = true;
                    this.m_audioSource.Play();
                }
            }
            else if (this.m_audioSource.isPlaying)
            {
                this.m_audioSource.Stop();
                this.m_audioSource.loop = false;
                this.m_audioSource.clip = null;
            }

            

            if (this.m_jumpTriggered)
            {
                this.m_jumpTriggered = false;
                if (this.m_isGrounded)
                {
                    this.m_jumpVelocity = this.m_playerData.JumpVelocity;
                    this.m_hasJumpVelocity = true;
                    this.m_audioSource.PlayOneShot(this.m_jumpAudioClip);
                }
            }
        }

        private void ApplyGravity(float deltaTime)
        {
            if (this.m_hasJumpVelocity)
            {
                this.m_jumpVelocity += deltaTime * Physics.gravity.y;
                if (this.m_jumpVelocity < 0)
                {
                    this.m_jumpVelocity = 0;
                    this.m_hasJumpVelocity = false; // we are at the peak of the trajectory => transition to falling
                }
            }
            this.m_characterController.Move(deltaTime * (Physics.gravity + Vector3.up * this.m_jumpVelocity));
        }

        private void UpdateLookDirection()
        {
            if (this.m_isLocomoting)
                this.m_characterController.transform.rotation = Quaternion.LookRotation(Vector3.right * Mathf.Sign(this.m_locomotionVelocity), Vector3.up);
        }

        private void UpdateAnimator()
        {
            if (this.m_isGrounded)
            {
                this.m_animator.SetBool(m_isWalkingHash, this.m_isLocomoting);
            }
            else if(this.m_hasJumpVelocity)
            {
                // set jumping upwards animation
            }
            else
            {
                // set falling animation
            }
        }
    }
}