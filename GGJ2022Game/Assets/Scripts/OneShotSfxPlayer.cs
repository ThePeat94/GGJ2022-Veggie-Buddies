using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Nidavellir
{
    [RequireComponent(typeof(AudioListener))]
    public class OneShotSfxPlayer : MonoBehaviour
    {
        public void PlayOneShot(AudioClip clipToPlay)
        {
            if (clipToPlay == null)
                return;
            this.StartCoroutine(this.PlayClipAndDestroySource(clipToPlay));
        }

        private IEnumerator PlayClipAndDestroySource(AudioClip clip)
        {
            var audioSource = this.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
            yield return new WaitForSeconds(clip.length);
            Destroy(audioSource);
        }
    }
}