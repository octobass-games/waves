using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Map
{
    public class RoomEntrance : MonoBehaviour
    {
        public RoomId RoomId;
        public UnityEvent<RoomId> OnRoomEntered;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                OnRoomEntered.Invoke(RoomId);
            }
        }
    }
}
