using Octobass.Waves.Map;
using UnityEngine;

namespace Octobass.Waves.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        public string Name;
        public RoomId Room;

        public bool AutoTrack = true;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (AutoTrack && collision.CompareTag(Tags.Player))
            {
                if (collision.gameObject.TryGetComponent(out SpawnTracker spawnTracker))
                {
                    spawnTracker.SetSpawnPoint(this);
                }
                else
                {
                    Debug.Log("[SpawnPoint]: Could not find SpawnTracker");
                }
            }
        }

        void OnValidate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = System.Guid.NewGuid().ToString();
            }
        }
    }
}
