using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] private AudioClip m_attackAudioClip;
        [SerializeField] private AudioMixerGroup m_audioMixerGroup;

        private AudioSource m_attackAudioSource;

        private void Awake()
        {
            this.m_attackAudioSource = this.gameObject.AddComponent<AudioSource>();
            this.m_attackAudioSource.clip = this.m_attackAudioClip;
            this.m_attackAudioSource.outputAudioMixerGroup = this.m_audioMixerGroup;
        }

        public void Attack()
        {
            this.m_attackAudioSource.Play();
        }
    }
}