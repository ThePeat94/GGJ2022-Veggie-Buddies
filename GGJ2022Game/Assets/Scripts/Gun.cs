using UnityEngine;
using UnityEngine.Audio;

namespace Nidavellir
{
    public class Gun : MonoBehaviour

    {
        [SerializeField] private Projectile m_projectileAsset;

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
            // TODO: use a pool for the projectiles, especially when adding more complex stuff like audio sources
            var projectile = Instantiate(this.m_projectileAsset);
            projectile.transform.position = this.transform.position;

            this.m_attackAudioSource.Play();

            // by adding the gun's (= the character's) velocity to the projectile, we can actively throw it further by moving while shooting
            projectile.Launch(this.transform.forward * 1000f);
        }
    }
}