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

        public void SwitchCamera(CinemachineCamera camera)
        {
            Camera.gameObject.SetActive(false);
            Camera = camera;
            Camera.gameObject.SetActive(true);
        }
    }
}
