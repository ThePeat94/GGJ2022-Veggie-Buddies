using UnityEngine;

namespace Nidavellir
{

    [RequireComponent(typeof(AttackTarget))]
    public class SpikyAgent : MonoBehaviour
    {
        [SerializeField] private float m_frequency;
        [SerializeField] private float m_amplitude;

        private CharacterController m_characterController;
        private AttackTarget m_attackTarget;

        private void Awake()
        {
            this.m_characterController = GetComponent<CharacterController>();
            this.m_attackTarget = GetComponent<AttackTarget>();
            this.m_attackTarget.onAttacked.AddListener(this.OnAttacked);
        }

        public void OnAttacked()
        {
            // TODO: play audio clip
            Destroy(this.gameObject);
        }

        void Update()
        {
            var dist = -this.m_amplitude * this.m_frequency * Mathf.Sin(Time.time * m_frequency);
            this.m_characterController.Move(Vector3.right * Time.deltaTime * dist);
        }
    }
}
