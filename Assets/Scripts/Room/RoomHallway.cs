using UnityEngine;

namespace Octobass.Waves.Room
{
    public class RoomHallway : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                foreach (IRoomHallwayWatcher hallwayWatcher in GetComponents<IRoomHallwayWatcher>())
                {
                    hallwayWatcher.OnEntrance();
                }
            }

        }
    }
}
