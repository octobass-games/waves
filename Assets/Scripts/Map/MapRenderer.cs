using Octobass.Waves.Room;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class MapRenderer : MonoBehaviour
    {
        public List<MapRoomRenderer> RoomRenderers;

        public bool miniMode = true;

        public GameObject MiniMapRows;
        public GameObject MiniMap;
        public GameObject BigMap;

        private Vector3 MiniMapCentre;

        void Awake()
        {
            MiniMapCentre = MiniMapRows.transform.position;
        }

        public void Draw(List<RoomInstance> rooms, RoomId activeRoom)
        {
            foreach (RoomInstance room in rooms)
            {
                MapRoomRenderer renderer = RoomRenderers.Find(renderer => renderer.Id == room.Id);

                if (renderer != null)
                {
                    renderer.Draw(room);

                    if (room.Id == activeRoom)
                    {
                        Vector3 translation = MiniMapCentre - renderer.transform.position;

                        MiniMapRows.transform.position += translation + new Vector3(0, renderer.GetComponent<SpriteRenderer>().size.y, 0);
                    }
                }
                else
                {
                    Debug.LogWarning($"[MapRenderer]: Could not find MapRoomRenderer for {room.Id}");
                }
            }
        }

        public void ToggleMode()
        {
            miniMode = !miniMode;

            if (miniMode)
            {
                MiniMap.SetActive(true);
                BigMap.SetActive(false);
            }
            else
            {
                MiniMap.SetActive(false);
                BigMap.SetActive(true);
            }
        }
    }
}
