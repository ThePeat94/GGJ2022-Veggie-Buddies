using System;
using UnityEngine;

namespace Nidavellir
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer s_instance;

        private AudioSource m_audioSource;
        
        public static MusicPlayer Instance => s_instance;

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            this.m_audioSource = this.GetComponent<AudioSource>();
        }

        public void PlayLoopingMusic(AudioClip toPlay)
        {
            this.PlayClip(toPlay, true);
        }

        public void PlayMusicOnce(AudioClip toPlay)
        {
            this.PlayClip(toPlay, false);
        }

        private void PlayClip(AudioClip toPlay, bool loop)
        {
            this.m_audioSource.clip = toPlay;
            this.m_audioSource.loop = loop;
            this.m_audioSource.Play();
        }
    }
}