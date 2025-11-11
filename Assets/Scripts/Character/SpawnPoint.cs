using UnityEngine;

namespace Octobass.Waves.Character
{
    public class SpawnPoint : MonoBehaviour
    {
        public string Name;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
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
    }
}
