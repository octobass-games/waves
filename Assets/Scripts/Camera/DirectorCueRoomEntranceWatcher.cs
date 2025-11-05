using Octobass.Waves.Room;
using Unity.Cinemachine;
using UnityEngine;

namespace Octobass.Waves.Camera
{
    public class DirectorCueRoomEntranceWatcher : MonoBehaviour, IRoomEntranceWatcher
    {
        public CameraDirector Director;
        public CinemachineCamera Camera;

        void Awake()
        {
            if (Camera == null)
            {
                Debug.LogWarning($"[DirectorCue]: Camera not set");
            }

            if (Director == null)
            {
                Debug.LogWarning($"[DirectorCue]: Camera director not set");
            }
        }

        public void OnEntrance()
        {
            Director.SwitchCamera(Camera);
        }
    }
}
