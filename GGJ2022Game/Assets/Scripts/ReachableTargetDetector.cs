using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class ReachableTargetDetector : MonoBehaviour
    {
        private List<AttackTarget> m_targetsInMeleeRange;

        public AttackTarget[] AttackTargetsInRange => this.m_targetsInMeleeRange.ToArray();

        private void Awake()
        {
            this.m_targetsInMeleeRange = new List<AttackTarget>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<AttackTarget>();
            Debug.Log($"OnTriggerEnter {target}");
            if (target != null)
            {
                m_targetsInMeleeRange.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.GetComponent<AttackTarget>();
            Debug.Log($"OnTriggerExit {target}");
            if (target != null)
            {
                m_targetsInMeleeRange.Remove(target);
            }
        }
    }
}