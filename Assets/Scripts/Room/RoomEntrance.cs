using UnityEngine;

namespace Octobass.Waves.Room
{
    public class RoomEntrance : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                foreach (IRoomEntranceWatcher entranceWatcher in GetComponents<IRoomEntranceWatcher>())
                {
                    entranceWatcher.OnEntrance();
                }
            }
            
        }
    }
}
