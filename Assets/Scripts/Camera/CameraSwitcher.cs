using Octobass.Waves.Map;
using Octobass.Waves.Spawn;
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
                RoomCamera.Priority = 0;
                
                if (RoomCamera.transform.childCount > 0)
                {
                    RoomCamera.transform.GetChild(0).gameObject.SetActive(false);
                }

                RoomCamera = camera;
                RoomCamera.Priority = 2;

                if (RoomCamera.transform.childCount > 0)
                {
                    RoomCamera.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.LogWarning($"[CameraSwitcher]: Could not find camera for room - {room}");
            }
        }

        public void OnUpgradeStart()
        {
            RoomCamera.Priority = 0;
            UpgradeCamera.Priority = 2;
        }

        public void OnUpgradeEnd()
        {
            UpgradeCamera.Priority = 0;
            RoomCamera.Priority = 2;
        }
    }
}
