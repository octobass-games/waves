using Octobass.Waves.Room;
using UnityEngine;

namespace Octobass.Waves.Camera
{
    public class DirectorCueRoomEntranceWatcher : MonoBehaviour, IRoomEntranceWatcher
    {
        public CameraDirector Director;
        public CameraSetup Setup;

        void Awake()
        {
            if (Setup == null)
            {
                Debug.LogWarning($"[DirectorCue]: Camera setup not set");
            }

            if (Director == null)
            {
                Debug.LogWarning($"[DirectorCue]: Camera director not set");
            }
        }

        public void OnEntrance()
        {
            Director.SetupCamera(Setup);
        }
    }
}
