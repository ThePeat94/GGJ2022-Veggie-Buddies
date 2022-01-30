using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class FallDownOnEnter : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_fallingObject;

        [SerializeField]
        private float m_timeTillFallingDown = 0.2f;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var playerController))
                return;

            if (this.m_fallingObject.GetComponent<Rigidbody>() != null)
                return;

            this.StartCoroutine(this.FallDown());
        }

        private IEnumerator FallDown()
        {
            var animator = this.m_fallingObject.GetComponent<Animator>();
            animator.enabled = true;
            animator.SetFloat("Speed", 1f);
            yield return new WaitForSeconds(this.m_timeTillFallingDown / 3);
            animator.SetFloat("Speed", 2f);
            yield return new WaitForSeconds(this.m_timeTillFallingDown / 3);
            animator.SetFloat("Speed", 3f);
            yield return new WaitForSeconds(this.m_timeTillFallingDown / 3);
            animator.enabled = false;
            this.m_fallingObject.AddComponent<Rigidbody>();
            var obstacle = this.m_fallingObject.AddComponent<DeadZone>();
            yield return new WaitForSeconds(2f);
            obstacle.enabled = false;
            this.m_fallingObject.AddComponent<PushAndPullAbility>();
        }
    }
}
