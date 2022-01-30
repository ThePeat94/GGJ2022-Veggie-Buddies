using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class DestroyOnHit : MonoBehaviour
    {
        private Coroutine m_destroying = null;

        private void OnTriggerEnter(Collider other)
        {
            if (this.m_destroying != null)
                return;

            Debug.Log($"DestoryOnHit triggered by: {other.gameObject}");
            var controller = other.gameObject.GetComponent<PlayerController>();
            if (controller != null)
            {
                this.m_destroying = this.StartCoroutine(this.DestroyRoutine());
            }
        }

        private IEnumerator DestroyRoutine()
        {
            var animator = this.GetComponent<Animator>();
            animator.enabled = true;
            animator.SetFloat("Speed", 0.25f);
            yield return new WaitForSeconds(0.5f);
            animator.SetFloat("Speed", 0.5f);
            yield return new WaitForSeconds(0.5f);
            animator.SetFloat("Speed", 0.75f);
            yield return new WaitForSeconds(0.5f);
            animator.SetFloat("Speed", 1f);
            yield return new WaitForSeconds(0.5f);
            this.gameObject.AddComponent<Rigidbody>();
            yield return new WaitForSeconds(4f);
            GameObject.Destroy(this.gameObject);

        }
    }
}
