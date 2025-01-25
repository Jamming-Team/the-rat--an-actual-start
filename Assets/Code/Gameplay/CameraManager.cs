using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Rat
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _followCamera;
        [SerializeField] private CinemachineCamera _overlookCamera;

        public void SetCurrentCamera(GC.Camera cameraToSet)
        {
            Debug.Log(cameraToSet);
            _followCamera.Priority.Enabled = false;
            _overlookCamera.Priority.Enabled = false;
            switch (cameraToSet)
            {
                case GC.Camera.CameraFollow:
                    _followCamera.Priority.Enabled = true;
                    _followCamera.Priority.Value = 10;
                    // CinemachineCore.IsLive(_followCamera);
                    break;
                case GC.Camera.CameraOverlook:
                    _overlookCamera.Priority.Enabled = true;
                    _overlookCamera.Priority.Value = 10;
                    // CinemachineCore.IsLive(_overlookCamera);
                    break;
            }
        }

        public void Setup(Transform playerTransform, GameLevelData.OverlookCameraData data)
        {
            _followCamera.Follow = playerTransform;
            
            _overlookCamera.transform.position = new Vector3(data.position.x, data.position.y, -10f);
            _overlookCamera.Lens.OrthographicSize = data.orthographicSize;
        }
        
    }
}