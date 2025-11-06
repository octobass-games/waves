using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Map
{
    public class RoomHallway : MonoBehaviour
    {
        public RoomId RoomHallwayId;
        public UnityEvent<RoomId> OnHallwayEntered;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                OnHallwayEntered.Invoke(RoomHallwayId);
            }
        }
    }
}
