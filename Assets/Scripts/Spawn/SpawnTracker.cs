using Octobass.Waves.Movement;
using Octobass.Waves.Save;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Spawn
{
    public class SpawnTracker : MonoBehaviour, ISavable
    {
        [SerializeField]
        private MovementController MovementController;

        [SerializeField]
        private List<SpawnPoint> Spawns;

        private SpawnPoint CurrentSpawnPoint;

        private const string SpawnPointSaveKey = "spawn-point";

        void Awake()
        {
            if (MovementController == null)
            {
                Debug.LogWarning("[SpawnTracker]: MovementController not set");
            }
        }

        public void Respawn()
        {
            if (CurrentSpawnPoint != null)
            {
                MovementController.ResetAtPosition(CurrentSpawnPoint.transform.position);
            }
            else
            {
                Debug.LogWarning("[SpawnTracker]: Does not have CurrentSpawnPoint set");
            }
        }

        public void SetSpawnPoint(SpawnPoint spawnPoint)
        {
            CurrentSpawnPoint = spawnPoint;
        }

        public void Load(SaveData saveData)
        {
            string spawnPointName = saveData.Load<string>(SpawnPointSaveKey);
            SpawnPoint spawnPoint = Spawns.Find(spawn => spawn.Name == spawnPointName);

            if (spawnPoint != null)
            {
                CurrentSpawnPoint = spawnPoint;
            }
            else
            {
                Debug.Log($"[SpawnTracker]: SpawnPoint not found with name - {spawnPointName}");
                CurrentSpawnPoint = Spawns[0];
            }
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(SpawnPointSaveKey, CurrentSpawnPoint.Name);
        }
    }
}
