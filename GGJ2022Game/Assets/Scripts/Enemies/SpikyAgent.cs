using UnityEngine;

namespace Nidavellir
{

    [RequireComponent(typeof(AttackTarget))]
    public class SpikyAgent : MonoBehaviour
    {
        [SerializeField] private float m_frequency;
        [SerializeField] private float m_amplitude;

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
            // TODO: play audio clip
            Destroy(this.gameObject);
        }

        void FixedUpdate()
        {
            var velocity = -this.m_amplitude * this.m_frequency * Mathf.Sin(Time.timeSinceLevelLoad * m_frequency);
            this.transform.Translate(Vector3.right * Time.fixedDeltaTime * velocity, Space.World);
        }
    }
}
