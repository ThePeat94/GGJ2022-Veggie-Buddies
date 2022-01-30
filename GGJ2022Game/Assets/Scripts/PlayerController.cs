using Cinemachine;
using Nidavellir.Scriptables;
using Nidavellir.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData m_playerData;
        [SerializeField] private AudioMixerGroup m_audioMixerGroup;
        [SerializeField] private PlayerType m_playerType;
        [SerializeField] private GameHUD m_hud;
        [SerializeField] private GameObject m_body;

        [SerializeField] private Gun m_gun;
        [SerializeField] private Sword m_sword;

        [SerializeField] private Animator m_explosion;

        [SerializeField] private AudioClip m_runningLoopAudioClip;
        [SerializeField] private AudioClip m_landAudioClip;
        [SerializeField] private RandomClipPlayer m_jumpRandomClipPlayer;
        [SerializeField] private RandomClipPlayer m_dieRandomClipPlayer;

        private CharacterController m_characterController;
        private InputProcessor m_inputProcessor;
        private Animator m_animator;
        private AudioSource m_runningLoopAudioSource;
        private AudioSource m_landAudioSource;

        private static readonly int s_isWalkingHash = Animator.StringToHash("IsWalking");
        private static readonly int s_jumpHash = Animator.StringToHash("Jump");
        private static readonly int s_die = Animator.StringToHash("Die");
        private static readonly int s_explode = Animator.StringToHash("Explode");

        private float m_locomotionVelocity = 0f;
        private bool m_isLocomoting;
        private float m_jumpVelocity = 0f;
        private bool m_hasJumpVelocity = false;
        private bool m_isGrounded = false;
        private bool m_touchedGroundAfterSpawn = false;

        private bool m_playJumpAnimation;

        private bool m_isDead;
        private bool m_preventMovement;

        private EventHandler m_playerDied;
        private Dictionary<ItemKind, int> m_itemCountsByItemKind = new();

        public PlayerType PlayerType => this.m_playerType;

        public event EventHandler OnPlayerDied
        {
            add => this.m_playerDied += value;
            remove => this.m_playerDied -= value;
        }

        public void KillPlayer()
        {
            if (this.m_isDead)
                return;

            this.m_dieRandomClipPlayer.PlayRandomOneShot();
            this.m_isDead = true;
            this.StartCoroutine(this.Die());
        }

        public void PlayerHurt()
        {
            this.KillPlayer();
        }

        public void PickUp(ItemKind itemKind)
        {
            switch (itemKind)
            {
                case ItemKind.PumpkinSeeds:
                    this.AddToInventory(itemKind);
                    this.m_hud.SetPumpkinSeedCount(this.m_itemCountsByItemKind[ItemKind.PumpkinSeeds]);
                    break;
            }
        }

        public void RespawnPlayer(Vector3 respawnPosition)
        {
            this.StartCoroutine(Respawn(respawnPosition));
        }
        
        private IEnumerator Die()
        {
            this.m_animator.SetTrigger(s_die);
            this.m_runningLoopAudioSource.Stop();
            this.m_landAudioSource.Stop();
            
            if (this.m_explosion != null)
            {
                yield return new WaitForSeconds(0.5f);
                this.m_explosion.SetTrigger(s_explode);
                yield return new WaitForSeconds(0.2f);
                this.m_body.SetActive(false);
                yield return new WaitForSeconds(1.0f);
            }

            this.m_playerDied?.Invoke(this, System.EventArgs.Empty);
        }

        private IEnumerator Respawn(Vector3 respawnPosition)
        {
            this.m_characterController.enabled = false;
            yield return new WaitForEndOfFrame();
            this.transform.position = respawnPosition;
            yield return new WaitForEndOfFrame();
            this.m_characterController.enabled = true;
            this.m_body.SetActive(true);
        }

        private void AddToInventory(ItemKind kind)
        {
            var currentValue = this.m_itemCountsByItemKind.ContainsKey(kind) ? this.m_itemCountsByItemKind[kind] : 0;
            this.m_itemCountsByItemKind[kind] = currentValue + 1;
        }

        private void Awake()
        {
            this.m_inputProcessor = this.GetComponent<InputProcessor>();
            this.m_characterController = this.GetComponent<CharacterController>();
            this.m_animator = this.GetComponent<Animator>();

            this.m_runningLoopAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_runningLoopAudioSource.clip = this.m_runningLoopAudioClip;
            this.m_runningLoopAudioSource.loop = true;
            this.m_runningLoopAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;

            this.m_landAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_landAudioSource.clip = this.m_landAudioClip;
            this.m_landAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;
        }

        private void Update()
        {
            if(this.m_isDead || this.m_preventMovement)
                return;
            
            this.ApplyGravity(Time.deltaTime); // we have to apply gravity first to make sure the CharacterController.isGrounded property works
            this.ApplyLocomotion(Time.deltaTime);
            this.ExecuteAttack();
            this.UpdateLookDirection();
        }
        private void LateUpdate()
        {
            if(this.m_isDead || this.m_preventMovement)
                return;
            this.UpdateAnimator();
        }

        private void ApplyLocomotion(float deltaTime)
        {
            var wasGrounded = this.m_isGrounded;
            this.m_isGrounded = this.m_characterController.isGrounded;

            if (!wasGrounded && this.m_isGrounded && this.m_touchedGroundAfterSpawn)
                this.m_landAudioSource.Play();

            if (!this.m_touchedGroundAfterSpawn && this.m_isGrounded)
                this.m_touchedGroundAfterSpawn = true;

            this.m_locomotionVelocity = this.m_inputProcessor.RunInput * this.m_playerData.MovementSpeed;
            this.m_isLocomoting = Mathf.Abs(this.m_locomotionVelocity) > Mathf.Epsilon;

            if (this.m_isLocomoting)
            {
                this.m_characterController.Move(Vector3.right * deltaTime * this.m_locomotionVelocity);
            }

            if (this.m_isGrounded && this.m_isLocomoting)
            {
                if (!this.m_runningLoopAudioSource.isPlaying)
                {
                    this.m_runningLoopAudioSource.Play();
                }
            }
            else if (this.m_runningLoopAudioSource.isPlaying)
            {
                this.m_runningLoopAudioSource.Stop();
            }

            if (this.m_inputProcessor.JumpTriggered && this.m_isGrounded)
            {
                this.m_jumpVelocity = this.m_playerData.JumpVelocity;
                this.m_hasJumpVelocity = true;
                this.m_playJumpAnimation = true;
                m_jumpRandomClipPlayer.PlayRandomOneShot();
            }
        }

        private void ExecuteAttack()
        {
            if (this.m_inputProcessor.AttackTriggered)
            {
                if (this.m_gun != null)
                {
                    this.m_gun.Attack();
                }
                if (this.m_sword != null)
                {
                    this.m_sword.Attack();
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
                this.m_animator.SetBool(s_isWalkingHash, this.m_isLocomoting);
            }
            else if(this.m_playJumpAnimation)
            {
                // set jumping upwards animation
                this.m_animator.SetTrigger(s_jumpHash);
                this.m_playJumpAnimation = false;
            }
            else
            {
                // set falling animation
            }
        }

        public void PreventMovement()
        {
            this.m_preventMovement = true;
        }
    }
}