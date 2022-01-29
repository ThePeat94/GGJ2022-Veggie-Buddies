﻿using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{
    public class RandomClipPlayer : MonoBehaviour
    {
        [SerializeField] private bool m_concurrentPlyback;
        [SerializeField] private AudioClip[] m_audioClips;
        [SerializeField] private AudioMixerGroup m_audioMixerGroup;

        private AudioSource[] m_audioSources;

        private void Awake()
        {
            var audioSourcesCount = this.m_concurrentPlyback ? m_audioClips.Length : 1;
            this.m_audioSources = new AudioSource[audioSourcesCount];

            for (var i = 0; i < audioSourcesCount; i++)
            {
                AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = this.m_audioMixerGroup;
                audioSource.playOnAwake = false;
                this.m_audioSources[i] = audioSource;
            }
        }

        public void PlayRandomOneShot()
        {
            var audioClipIndex = Random.Range(0, (int)this.m_audioClips.Length);
            var audioSourceIndex = this.m_concurrentPlyback ? audioClipIndex : 0;
            this.m_audioSources[audioSourceIndex].PlayOneShot(this.m_audioClips[audioClipIndex]);
        }
    }
}