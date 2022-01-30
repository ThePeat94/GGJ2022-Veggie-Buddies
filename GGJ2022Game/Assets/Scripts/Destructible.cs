using UnityEngine;

namespace Nidavellir
{
    /// <summary>
    /// Destroys itself when attacked. Created for debug purposes. Left here for usage documentation.
    /// </summary>
    [RequireComponent(typeof(AttackTarget))]
    public class Destructible : MonoBehaviour
    {
        private AttackTarget m_attackTarget;

        private void Awake()
        {
            this.m_attackTarget = GetComponent<AttackTarget>();
        }

        private void Start()
        {
            this.m_attackTarget.onAttacked.AddListener(this.OnAttacked);
        }

        public void OnAttacked()
        {
            Destroy(this.gameObject);
        }
    }
}
