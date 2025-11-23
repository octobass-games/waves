using UnityEngine;
using UnityEngine.UI;

namespace Octobass.Waves.Map
{
    public class MapRoomRenderer : MonoBehaviour
    {
        public RoomId Id;

        private Image Image;
        private float Opacity;

        private MapRoomDetailsRenderer detailsRenderer;

        void Awake()
        {
            Image = GetComponent<Image>();

            detailsRenderer = GetComponentInChildren<MapRoomDetailsRenderer>();
        }

        public void Draw(Room room, bool isPlayerInRoom, bool miniMode)
        {
            Color color = Image.color;

            if (room.State == RoomState.Unknown)
            {
                Image.enabled = false;
            }
            else
            {
                Image.enabled = true;
                Image.color = new Color(color.r, color.g, color.b, room.State == RoomState.Discovered ? 0.5f : 1f); 
            }
            if (!miniMode)
            {
                detailsRenderer.Player.SetActive(isPlayerInRoom);
            }
            if (room.IsShellFound)
            {
                detailsRenderer.Shell.SetActive(true);
            }
        }
    }
}
