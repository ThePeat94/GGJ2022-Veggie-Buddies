using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class SpikyAgent : MonoBehaviour
    {
        [SerializeField] private float m_frequency;
        [SerializeField] private float m_amplitude;

        private CharacterController m_characterController;

        private void Awake()
        {
            this.m_characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            var dist = -this.m_amplitude * this.m_frequency * Mathf.Sin(Time.time * m_frequency);
            this.m_characterController.Move(Vector3.right * Time.deltaTime * dist);
        }
    }
}
