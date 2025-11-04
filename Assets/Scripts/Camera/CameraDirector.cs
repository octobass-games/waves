using Unity.Cinemachine;
using UnityEngine;

namespace Octobass.Waves.Camera
{
    public class CameraDirector : MonoBehaviour
    {
        public CinemachineCamera Camera;

        void Awake()
        {
            if (Camera == null)
            {
                Debug.LogWarning($"[CameraDirector]: Camera not set");
            }
        }

        public void SetupCamera(CameraSetup setup)
        {
            Debug.Log($"[CameraDirector]: Focusing camera to look at {setup.Anchor.name}");

            Camera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = setup.Bounds;
            Camera.Follow = setup.Anchor;
            Camera.LookAt = setup.Anchor;
        }
    }
}
