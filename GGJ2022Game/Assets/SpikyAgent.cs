using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class SpikyAgent : MonoBehaviour
    {
        [SerializeField] private float m_movementWidth;
        [SerializeField] private float m_movementSpeed;

        private Vector3 m_spawnPosition;
        private float m_currentDirection = 1f;

        private void Start()
        {
            this.m_spawnPosition = this.transform.position;
        }

        void Update()
        {
            var destX = this.m_spawnPosition.x + (this.m_movementWidth *.5f * this.m_currentDirection);
            var remainingDist = this.transform.position.x - destX;
            var travelDist = this.m_movementSpeed * Time.deltaTime;
        }
    }
}
