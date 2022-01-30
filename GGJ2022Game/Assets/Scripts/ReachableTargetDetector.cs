using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class ReachableTargetDetector : MonoBehaviour
    {
        private List<AttackTarget> m_targetsInMeleeRange;
        private List<PushPullable> m_pushPullablesInMeleeRange;

        public AttackTarget[] AttackTargetsInRange => this.m_targetsInMeleeRange.ToArray();
        public PushPullable[] PushPullablesInRange => this.m_pushPullablesInMeleeRange.ToArray();

        private void Awake()
        {
            this.m_targetsInMeleeRange = new List<AttackTarget>();
            this.m_pushPullablesInMeleeRange = new List<PushPullable>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<AttackTarget>();
            Debug.Log($"OnTriggerEnter {target}");
            if (target != null)
            {
                m_targetsInMeleeRange.Add(target);
            }

            var pushPullable = other.GetComponent<PushPullable>();
            if (pushPullable != null)
            {
                this.m_pushPullablesInMeleeRange.Add(pushPullable);
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

            var pushPullable = other.GetComponent<PushPullable>();
            if (pushPullable != null)
            {
                this.m_pushPullablesInMeleeRange.Remove(pushPullable);
            }
        }
    }
}