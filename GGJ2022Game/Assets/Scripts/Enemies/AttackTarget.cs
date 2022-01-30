using UnityEngine;
using UnityEngine.Events;

namespace Nidavellir
{
    public class AttackTarget : MonoBehaviour
    {
        public UnityEvent onAttacked;

        private void OnCollisionEnter(Collision collision)
        {
            var projectile = collision.gameObject.GetComponent<Projectile>();
            if (projectile != null)
                this.Attack();
        }

        private void Awake()
        {
            this.onAttacked = new UnityEvent();
        }

        public void Attack()
        {
            onAttacked.Invoke();
        }
    }
}
