using System;
using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir
{
    public class PlayerController : MonoBehaviour
    {
        private static PlayerController s_instance;

        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private PlayerType m_playerType;

        private Vector3 m_moveDirection;
        private CharacterController m_characterController;
        private InputProcessor m_inputProcessor;
        private Animator m_animator;
        private GameObject m_currentInteractable;

        private EventHandler m_playerDied;

        private static readonly int s_isWalkingHash = Animator.StringToHash("IsWalking");
        

        public PlayerType PlayerType => this.m_playerType;
        public event EventHandler OnPlayerDied
        {
            add => this.m_playerDied += value;
            remove => this.m_playerDied -= value;
        }
    
    
        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_characterController = this.GetComponent<CharacterController>();
            this.m_animator = this.GetComponent<Animator>();
        }
    
        // Update is called once per frame
        void Update()
        {
            this.Move();
            this.Rotate();
        }

        private void LateUpdate()
        {
            this.UpdateAnimator();
        }

        public void PlayerHurt()
        {
            this.m_playerDied?.Invoke(this, System.EventArgs.Empty);
        }
    
        protected void Move()
        {
            this.m_moveDirection = new Vector3(this.m_inputProcessor.Movement.x, Physics.gravity.y, this.m_inputProcessor.Movement.y);
            this.m_characterController.Move(this.m_moveDirection * Time.deltaTime * this.m_playerData.MovementSpeed);
        }
        
        private void Rotate()
        {
            var targetDir = this.m_moveDirection;
            targetDir.y = 0f;

            if (targetDir == Vector3.zero)
                targetDir = this.transform.forward;
    
            this.RotateTowards(targetDir);
        }

        private void RotateTowards(Vector3 dir)
        {
            var lookRotation = Quaternion.LookRotation(dir.normalized);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookRotation, this.m_playerData.RotationSpeed * Time.deltaTime);
        }

        private void UpdateAnimator()
        {
            this.m_animator.SetBool(s_isWalkingHash, this.m_moveDirection != Physics.gravity);
        }
    }
}
