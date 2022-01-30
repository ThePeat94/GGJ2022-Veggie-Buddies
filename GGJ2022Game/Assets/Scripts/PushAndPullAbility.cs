using UnityEngine;

namespace Nidavellir
{
    public class PushAndPullAbility : MonoBehaviour
    {
        [SerializeField] private ReachableTargetDetector m_reachableTargetDetector;

        private PushPullable[] m_controlledPushPullables = null;
        private Vector3[] m_pushPullableOffsets = null;
        private int[] m_originalLayers = null;
        private bool m_active = false;
        private int m_activelyPushedPulledLayer;

        private void Awake()
        {
            this.m_activelyPushedPulledLayer = LayerMask.NameToLayer("ActivelyPushedPulled");
        }

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
            Debug.Log("PushAndPullAbility.Activate()");
            this.m_controlledPushPullables = this.m_reachableTargetDetector.PushPullablesInRange;
            this.m_active = true;
            this.m_pushPullableOffsets = new Vector3[this.m_controlledPushPullables.Length];
            this.m_originalLayers = new int[this.m_controlledPushPullables.Length];

            for (int i = 0; i < m_controlledPushPullables.Length; i++)
            {
                Debug.Log($"{this.m_controlledPushPullables[i]}");
                var pushPullable = this.m_controlledPushPullables[i];
                this.m_originalLayers[i] = pushPullable.gameObject.layer;
                pushPullable.gameObject.layer = this.m_activelyPushedPulledLayer;
                this.m_pushPullableOffsets[i] = pushPullable.transform.position - this.transform.position;
            }
        }

        internal void Deactivate()
        {
            Debug.Log("PushAndPullAbility.Deactivate()");
            this.m_controlledPushPullables = null;
            this.m_pushPullableOffsets = null;
            this.m_active = false;
        }
    }
}