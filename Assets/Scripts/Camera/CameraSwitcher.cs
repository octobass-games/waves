using Octobass.Waves.Map;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace Octobass.Waves.Camera
{
    public class CameraSwitcher : MonoBehaviour
    {
        [SerializeField]
        private CinemachineCamera RoomCamera;

        [SerializeField]
        private CinemachineCamera UpgradeCamera;

        [SerializeField]
        private List<RoomCameraBinding> RoomCameraBindings;

        private Dictionary<RoomId, CinemachineCamera> RoomCameraBindingRegistry;

        void Awake()
        {
            if (RoomCamera == null)
            {
                Debug.LogWarning($"[CameraSwitcher]: Initial RoomCamera not set");
            }

            if (UpgradeCamera == null)
            {
                Debug.LogWarning($"[CameraSwitcher]: UpgradeCamera not set");
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
                RoomCamera.gameObject.SetActive(false);
                RoomCamera = camera;
                RoomCamera.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"[CameraSwitcher]: Could not find camera for room - {room}");
            }
        }

        public void OnUpgradeStart()
        {
            RoomCamera.gameObject.SetActive(false);
            UpgradeCamera.gameObject.SetActive(true);
        }

        public void OnUpgradeEnd()
        {
            UpgradeCamera.gameObject.SetActive(false);
            RoomCamera.gameObject.SetActive(true);
        }
    }
}
