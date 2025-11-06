using UnityEngine;
using UnityEngine.UI;

namespace Octobass.Waves.Map
{
    public class MapRoomRenderer : MonoBehaviour
    {
        public RoomId Id;

        private Image Image;

        void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void Draw(Room room)
        {
            Color color = Image.color;
            
            switch (room.State)
            {
                case RoomState.Unknown:
                    Image.enabled = false;
                    break;
                case RoomState.Discovered:
                    Image.enabled = true;
                    Image.color = new Color(color.r, color.g, color.b, 0.5f);
                    break;
                case RoomState.Visited:
                    Image.enabled = true;
                    Image.color = new Color(color.r, color.g, color.b, 1f);
                    break;
            }
        }
    }
}
