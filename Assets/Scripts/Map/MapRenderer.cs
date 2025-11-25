using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Octobass.Waves.Map
{
    public class MapRenderer : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput PlayerInput;

        public List<MapRoomRenderer> RoomRenderers;
        public List<MapRoomRenderer> BigMapRenderers;
        public Button CloseButton;

        public bool miniMode = true;
        public bool HideOnAwake = true;

        public GameObject MiniMapRows;
        public GameObject MiniMap;
        public GameObject MiniMask;
        public GameObject BigMap;

        private Vector3 MiniMapCentre;

        private List<Room> Rooms = new();
        private RoomId ActiveRoom;

        void Awake()
        {
            MiniMapCentre = MiniMapRows.transform.position;

            if (HideOnAwake)
            {
                HideMap();
            }
        }

        void Update()
        {
            if (PlayerInput.actions.FindAction("InspectMap").WasPressedThisFrame())
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

        private void Draw(bool teleportMode = false)
        {
            if (miniMode)
            {
                foreach (Room room in Rooms)
                {
                    MapRoomRenderer renderer = RoomRenderers.Find(renderer => renderer.Id == room.Id);
                    bool playerInRoom = room.Id == ActiveRoom;

                    if (renderer != null)
                    {
                        renderer.Draw(room, playerInRoom, miniMode = true, false);

                        if (playerInRoom)
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
                    bool playerInRoom = room.Id == ActiveRoom;

                    if (renderer != null)
                    {
                        renderer.Draw(room, playerInRoom, miniMode = false, teleportMode);
                        if (room.Id == ActiveRoom)
                        {
                            EventSystem.current.SetSelectedGameObject(renderer.gameObject);
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"[MapRenderer]: Could not find MapRoomRenderer for {room.Id}");
                    }
                }
            }

            if (teleportMode)
            {
                CloseButton.gameObject.SetActive(true);
            }
            else
            {
                CloseButton.gameObject.SetActive(false);
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

        public void ShowTeleportMap()
        {
            if (miniMode)
            {
                ToggleMode();
            }

            Draw(true);
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
