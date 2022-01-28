using Scriptables;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData m_playerData;

    private CharacterController m_characterController;
    private InputProcessor m_inputProcessor;
    private Animator m_animator;
    
    private readonly int m_isWalkingHash = Animator.StringToHash("IsWalking");

    private bool m_jumpTriggered = false;

    private Vector3 m_jumpVelocity = Vector3.zero;

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

    private Vector3 LocomotionVector => this.m_inputProcessor.RunInput * this.m_playerData.MovementSpeed * Vector3.right;

    protected void ApplyLocomotion(float deltaTime)
    {
        // we may only call Move with a non-zero vector in order to make sure the CharacterController.isGrounded property works
        if (this.LocomotionVector.sqrMagnitude > Mathf.Epsilon)
            this.m_characterController.Move(deltaTime * this.LocomotionVector);

        if (this.m_jumpTriggered)
        {
            this.m_jumpTriggered = false;
            if (this.m_characterController.isGrounded)
            {
                this.m_characterController.SimpleMove(Vector3.up * this.m_playerData.JumpForce);
            }
        }
    }

    protected void ApplyGravity(float deltaTime)
    {
        this.m_characterController.Move(deltaTime * Physics.gravity);
    }
    
    protected void UpdateLookDirection()
    {
        if (this.LocomotionVector.sqrMagnitude > Mathf.Epsilon)
            this.m_characterController.transform.rotation = Quaternion.LookRotation(this.LocomotionVector.normalized, Vector3.up);
    }

    private void UpdateAnimator()
    {
        this.m_animator.SetBool(m_isWalkingHash, this.LocomotionVector.sqrMagnitude > Mathf.Epsilon);
    }
}
