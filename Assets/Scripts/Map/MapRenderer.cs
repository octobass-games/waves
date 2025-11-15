using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class MapRenderer : MonoBehaviour
    {
        public List<MapRoomRenderer> RoomRenderers;
        public List<MapRoomRenderer> BigMapRenderers;

        public bool miniMode = true;
        public bool HideOnAwake = true;

        public GameObject MiniMapRows;
        public GameObject MiniMap;
        public GameObject MiniMask;
        public GameObject BigMap;

        private Vector3 MiniMapCentre;
        private PlayerInput PlayerInput;

        private List<Room> Rooms = new();
        private RoomId ActiveRoom;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            MiniMapCentre = MiniMapRows.transform.position;

            if (HideOnAwake)
            {
                HideMap();
            }
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
            Rooms = rooms;
            ActiveRoom = activeRoom;

            Draw();
        }

        private void Draw()
        {
            if (miniMode)
            {
                foreach (Room room in Rooms)
                {
                    MapRoomRenderer renderer = RoomRenderers.Find(renderer => renderer.Id == room.Id);

                    if (renderer != null)
                    {
                        renderer.Draw(room);

                        if (room.Id == ActiveRoom)
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
                foreach (Room room in Rooms)
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

            Draw();
        }

        public void ShowMap()
        {
            if (miniMode)
            {
                MiniMap.SetActive(true);
                MiniMask.SetActive(true);
            }
            else
            {
                BigMap.SetActive(true);
            }
        }

        public void HideMap()
        {
            if (miniMode)
            {
                MiniMap.SetActive(false);
                MiniMask.SetActive(false);
            }
            else
            {
                BigMap.SetActive(false);
            }
        }
    }
}
