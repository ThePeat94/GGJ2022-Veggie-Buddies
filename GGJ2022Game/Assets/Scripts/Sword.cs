using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private AudioClip m_swingAudioClip;
        [SerializeField] private AudioClip m_hitAudioClip;
        [SerializeField] private AudioMixerGroup m_audioMixerGroup;

        private List<AttackTarget> m_targetsInMeleeRange;

        private AudioSource m_swingAudioSource;
        private AudioSource m_hitAudioSource;

        private void Awake()
        {
            this.m_hitAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_hitAudioSource.clip = this.m_hitAudioClip;
            this.m_hitAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;

            this.m_swingAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_swingAudioSource.clip = this.m_swingAudioClip;
            this.m_swingAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;

            this.m_targetsInMeleeRange = new List<AttackTarget>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<AttackTarget>();
            if (target != null)
            {
                m_targetsInMeleeRange.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<AttackTarget>();
            if (target != null)
            {
                m_targetsInMeleeRange.Remove(target);
            }
        }

        public void Attack()
        {
            this.m_swingAudioSource.Play();
            if (this.m_targetsInMeleeRange.Count > 0)
            {
                this.m_hitAudioSource.Play();
                foreach (var target in this.m_targetsInMeleeRange)
                {
                    target.Attack();
                }
            }
        }
    }
}