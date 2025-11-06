using UnityEngine;
using UnityEngine.UI;

namespace Octobass.Waves.Map
{
    public class MapRoomRenderer : MonoBehaviour
    {
        public RoomId Id;

        private Image Image;
        private float Opacity;

        void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void Draw(Room room)
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
        }
    }
}
