using UnityEngine;

namespace Nidavellir
{
    public class PushAndPullAbility : MonoBehaviour
    {
        [SerializeField] private ReachableTargetDetector m_reachableTargetDetector;

        private GameObject m_controllingGameObject = null;
        private PushPullable[] m_controlledPushPullables = null;
        private bool m_active = false;

        private void FixedUpdate()
        {
            
        }

        internal void Activate(GameObject actingGameObject)
        {
            this.m_controllingGameObject = actingGameObject;
            this.m_controlledPushPullables = this.m_reachableTargetDetector.GetPushPullables();
            this.m_active = true;
        }

        internal void Deactivate()
        {
            this.m_controllingGameObject = null;
            this.m_active = false;
        }
    }
}