using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    [ExecuteInEditMode]
    public class SideScrollCameraFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform m_target;

        [SerializeField]
        private Vector3 m_cameraOffset;

        private void OnEnable()
        {
            this.transform.position = this.GetDesiredPosition();
        }

        private void LateUpdate()
        {
            if (this.m_target == null)
                return;

            this.transform.position = Vector3.Lerp(this.transform.position, this.GetDesiredPosition(), Mathf.Clamp(Time.deltaTime * 4f, 0, 1));
        }

        private Vector3 GetDesiredPosition()
        {
            var desiredPosition = this.transform.position;
            desiredPosition.x = this.m_target.position.x + this.m_cameraOffset.x;
            desiredPosition.y = this.m_target.position.y + this.m_cameraOffset.y;
            return desiredPosition;
        }
    }
}
