using UnityEngine;
using Cinemachine;

namespace Nidavellir
{
    // https://forum.unity.com/threads/cinemachine-follow-player-lock-axis.937118/
    /// <summary>
    /// An add-on module for Cinemachine Virtual Camera that flips the camera Up side down
    /// </summary>
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")] // Hide in menu
    public class FlipVirtualCamera : CinemachineExtension
    {
        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Finalize)
            {
                var rotation = state.RawOrientation.eulerAngles;
                state.RawOrientation = Quaternion.Euler(rotation.x, rotation.y, 180);
            }
        }
    }
}
