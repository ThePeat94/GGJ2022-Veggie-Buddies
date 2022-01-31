﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nidavellir
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer s_instance;

        private AudioSource m_audioSource;

        [SerializeField] private AudioClip m_titleTheme;
        [SerializeField] private AudioClip m_gameThemeIntro;
        [SerializeField] private AudioClip m_gameTheme;

        private List<AudioClip> m_gameThemeClips;
        private Coroutine m_queueRoutine;

        private int m_lastLoadedSceneIndex;

        public static MusicPlayer Instance => s_instance;

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
                DontDestroyOnLoad(this);
                SceneManager.sceneLoaded += this.SceneChanged;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }

            this.m_audioSource = this.GetComponent<AudioSource>();
            this.m_gameThemeClips = new()
            {
                this.m_gameThemeIntro,
                this.m_gameTheme
            };
        }

        private void SceneChanged(Scene loadedScene, LoadSceneMode arg1)
        {
            var hasLoadedMainMenu = loadedScene.buildIndex == 0;
            var hasSceneChanged = this.m_lastLoadedSceneIndex != loadedScene.buildIndex;
            if (hasLoadedMainMenu)
            {
                this.PlayLoopingMusic(this.m_titleTheme);
            }
            else if(hasSceneChanged)
            {
                this.PlayClips(this.m_gameThemeClips);
            }
            
            this.m_lastLoadedSceneIndex = loadedScene.buildIndex;
        }

        public void PlayLoopingMusic(AudioClip toPlay)
        {
            if (this.m_queueRoutine != null)
            {
                this.StopCoroutine(this.m_queueRoutine);
                this.m_queueRoutine = null;
            }
            
            this.PlayClip(toPlay, true);
        }

        public void PlayMusicOnce(AudioClip toPlay)
        {
            if (this.m_queueRoutine != null)
            {
                this.StopCoroutine(this.m_queueRoutine);
                this.m_queueRoutine = null;
            }
            
            this.PlayClip(toPlay, false);
        }

        public void PlayClips(List<AudioClip> clipQueue)
        {
            if (this.m_queueRoutine != null)
            {
                this.StopCoroutine(this.m_queueRoutine);
                this.m_queueRoutine = null;
                this.m_audioSource.Stop();
            }
            
            this.m_audioSource.loop = true;
            this.m_queueRoutine = this.StartCoroutine(this.PlayQueue(clipQueue));
        }

        private void PlayClip(AudioClip toPlay, bool loop)
        {
            this.m_audioSource.clip = toPlay;
            this.m_audioSource.loop = loop;
            this.m_audioSource.Play();
        }

        private IEnumerator PlayQueue(List<AudioClip> toPlay)
        {
            foreach (var current in toPlay)
            {
                this.m_audioSource.clip = current;
                this.m_audioSource.Play();
                yield return new WaitForSeconds(current.length);
            }
        }
        
    }
}