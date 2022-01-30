using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private AudioClip m_swingAudioClip;
        [SerializeField] private AudioClip m_hitAudioClip;
        [SerializeField] private AudioMixerGroup m_audioMixerGroup;
        [SerializeField] private ReachableTargetDetector m_reachableTargetDetector;

        private Animator m_animator;
        private AudioSource m_swingAudioSource;
        private AudioSource m_hitAudioSource;

        private void Awake()
        {
            this.m_animator = this.GetComponent<Animator>();

            this.m_hitAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_hitAudioSource.clip = this.m_hitAudioClip;
            this.m_hitAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;

            this.m_swingAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_swingAudioSource.clip = this.m_swingAudioClip;
            this.m_swingAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;
        }

        public void Attack()
        {
            this.m_swingAudioSource.Play();
            this.m_animator.SetTrigger("Swing");

            var attackTargetsInRange =  this.m_reachableTargetDetector.AttackTargetsInRange;
            if (attackTargetsInRange.Length > 0)
            {
                this.m_hitAudioSource.Play();
                foreach (var target in attackTargetsInRange)
                {
                    target.Attack();
                }
            }
        }
    }
}