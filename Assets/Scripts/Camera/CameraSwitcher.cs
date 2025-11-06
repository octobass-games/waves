using Octobass.Waves.Map;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Octobass.Waves.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField]
        private CinemachineCamera ActiveCamera;

        [SerializeField]
        private List<RoomCameraBinding> RoomCameraBindings;

        private Dictionary<RoomId, CinemachineCamera> RoomCameraBindingRegistry;

        void Awake()
        {
            if (ActiveCamera == null)
            {
                Debug.LogWarning($"[CameraSwitcher]: Initial camera not set");
            }

            RoomCameraBindingRegistry = new();

            foreach (RoomCameraBinding binding in RoomCameraBindings)
            {
                if (RoomCameraBindingRegistry.ContainsKey(binding.Room))
                {
                    Debug.LogWarning($"[CameraSwitcher]: Multiple bindings found for room - {binding.Room}");
                }

                RoomCameraBindingRegistry[binding.Room] = binding.Camera;
            }
        }

        public void OnRoomEntered(RoomId room)
        {
            if (RoomCameraBindingRegistry.TryGetValue(room, out CinemachineCamera camera))
            {
                ActiveCamera.gameObject.SetActive(false);
                ActiveCamera = camera;
                ActiveCamera.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"[CameraSwitcher]: Could not find camera for room - {room}");
            }
        }
    }
}
