using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class SpawnObstacle : MonoBehaviour
    {
        [SerializeField]
        private float m_spawnDelay = 2;

        [SerializeField]
        private float m_destroyObstacleAfter = 20;

        [SerializeField]
        private GameObject m_obstaclePrefab;

        [SerializeField]
        private Transform m_spawnPosition;

        private Coroutine m_spawnRoutine;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var playerController))
                return;

            if (this.m_spawnRoutine != null)
                return;

            this.m_spawnRoutine = this.StartCoroutine(this.SpawnObstacles());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<PlayerController>(out var playerController))
                return;

            this.StopCoroutine(this.m_spawnRoutine);
        }

        private IEnumerator SpawnObstacles()
        {
            while (true)
            {
                var obstacle = GameObject.Instantiate(this.m_obstaclePrefab, this.m_spawnPosition.position, this.m_spawnPosition.rotation);
                obstacle.SetActive(true);
                GameObject.Destroy(obstacle, this.m_destroyObstacleAfter);
                yield return new WaitForSeconds(this.m_spawnDelay);
            }
        }

    }
}
