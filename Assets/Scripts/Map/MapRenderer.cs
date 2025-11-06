using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class MapRenderer : MonoBehaviour
    {
        public List<MapRoomRenderer> RoomRenderers;
        public List<MapRoomRenderer> BigMapRenderers;

        public bool miniMode = true;

        public GameObject MiniMapRows;
        public GameObject MiniMap;
        public GameObject MiniMask;
        public GameObject BigMap;

        private Vector3 MiniMapCentre;
        private PlayerInput PlayerInput;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            MiniMapCentre = MiniMapRows.transform.position;
        }

        void Update()
        {
            if (PlayerInput.Movement.InspectMap.WasPressedThisFrame())
            {
                ToggleMode();
            }
        }

        public void OnRoomStateChanged(List<Room> rooms, RoomId activeRoom)
        {
            if (miniMode)
            {
                foreach (Room room in rooms)
                {
                    MapRoomRenderer renderer = RoomRenderers.Find(renderer => renderer.Id == room.Id);

                    if (renderer != null)
                    {
                        renderer.Draw(room);

                        if (room.Id == activeRoom)
                        {
                            Vector3 translation = MiniMapCentre - renderer.transform.position;

                            MiniMapRows.transform.position += translation;
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"[MapRenderer]: Could not find MapRoomRenderer for {room.Id}");
                    }
                }
            }
            else
            {
                foreach (Room room in rooms)
                {
                    MapRoomRenderer renderer = BigMapRenderers.Find(renderer => renderer.Id == room.Id);

                    if (renderer != null)
                    {
                        renderer.Draw(room);
                    }
                    else
                    {
                        Debug.LogWarning($"[MapRenderer]: Could not find MapRoomRenderer for {room.Id}");
                    }
                }
            }
        }

        public void ToggleMode()
        {
            miniMode = !miniMode;

            if (miniMode)
            {
                MiniMap.SetActive(true);
                MiniMask.SetActive(true);
                BigMap.SetActive(false);
            }
            else
            {
                MiniMap.SetActive(false);
                MiniMask.SetActive(false);
                BigMap.SetActive(true);
            }
        }
    }
}
