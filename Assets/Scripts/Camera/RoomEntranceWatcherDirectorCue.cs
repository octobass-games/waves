using Octobass.Waves.Room;
using UnityEngine;

namespace Octobass.Waves.Camera
{
    public class RoomEntranceWatcherDirectorCue : MonoBehaviour, IRoomEntranceWatcher
    {
        public CameraDirector Director;
        public CameraSetup Setup;

        void Awake()
        {
            if (Setup == null)
            {
                Debug.LogWarning($"[DirectorCue]: Camera setup missing from {name}");
            }

            if (Director == null)
            {
                Debug.LogWarning($"[DirectorCue]: Camera director missing from {name}");
            }
        }

        public void OnEntrance()
        {
            Director.SetupCamera(Setup);
        }
    }
}
