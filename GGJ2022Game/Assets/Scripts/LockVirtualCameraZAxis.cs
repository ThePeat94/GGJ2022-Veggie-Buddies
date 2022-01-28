using UnityEngine;
using Cinemachine;

namespace Nidavellir
{
    // https://forum.unity.com/threads/cinemachine-follow-player-lock-axis.937118/
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class LockVirtualCameraZAxis : CinemachineExtension
    {
        [Tooltip("Lock the camera's Z position to this value")]
        public float m_zPosition = 0;

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Finalize)
            {
                var pos = state.RawPosition;
                pos.z = this.m_zPosition;
                state.RawPosition = pos;
            }
        }
    }
}
