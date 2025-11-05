using Octobass.Waves.Room;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class MapRoomRenderer : MonoBehaviour
    {
        public RoomId Id;

        private SpriteRenderer spriteRenderer;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Draw(RoomInstance room)
        {
            Color color = spriteRenderer.color;
            
            switch (room.State)
            {
                case RoomState.Unknown:
                    spriteRenderer.enabled = false;
                    break;
                case RoomState.Discovered:
                    spriteRenderer.enabled = true;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 0.5f);
                    break;
                case RoomState.Visited:
                    spriteRenderer.enabled = true;
                    spriteRenderer.color = new Color(color.r, color.g, color.b, 1f);
                    break;
            }
        }
    }
}
