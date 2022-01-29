using UnityEngine;

namespace Nidavellir
{
    public class PushAndPullAbility : MonoBehaviour
    {
        [SerializeField] private ReachableTargetDetector m_reachableTargetDetector;

        private PushPullable[] m_controlledPushPullables = null;
        private Vector3[] m_pushPullableOffsets = null;
        private bool m_active = false;

        private void FixedUpdate()
        {
            if (this.m_active)
            {
                for (int i = 0; i < m_controlledPushPullables.Length; i++)
                {
                    this.m_controlledPushPullables[i].SetPosition(this.transform.position + this.m_pushPullableOffsets[i]);
                }
            }
        }

        internal void Activate()
        {
            this.m_controlledPushPullables = this.m_reachableTargetDetector.PushPullablesInRange;
            this.m_active = true;
            this.m_pushPullableOffsets = new Vector3[this.m_controlledPushPullables.Length];

            for (int i = 0; i < m_controlledPushPullables.Length; i++)
            {
                this.m_pushPullableOffsets[i] = this.m_controlledPushPullables[i].transform.position - this.transform.position;
            }
        }

        internal void Deactivate()
        {
            this.m_controlledPushPullables = null;
            this.m_pushPullableOffsets = null;
            this.m_active = false;
        }
    }
}