using Octobass.Waves.Camera;
using Octobass.Waves.Movement;
using Octobass.Waves.Save;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Octobass.Waves.Spawn
{
    public class SpawnTracker : MonoBehaviour, ISavable
    {
        [SerializeField]
        private MovementController MovementController;

        [SerializeField]
        private CameraSwitcher CameraSwitcher;

        [SerializeField]
        private SpawnPoint DefaultSpawnPoint;

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
                Vector2 bottomOfSpawnPoint = new(CurrentSpawnPoint.transform.position.x, CurrentSpawnPoint.GetComponent<BoxCollider2D>().bounds.min.y);
                MovementController.ResetAtPosition(bottomOfSpawnPoint);

                CameraSwitcher.OnRoomEntered(CurrentSpawnPoint.Room);
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

            List<SpawnPoint> spawnPoints = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None).ToList();
            SpawnPoint spawnPoint = spawnPoints.Find(spawnPoint => spawnPoint.Name == spawnPointName);

            if (spawnPoint != null)
            {
                CurrentSpawnPoint = spawnPoint;
            }
            else
            {
                Debug.Log($"[SpawnTracker]: SpawnPoint not found with name - {spawnPointName}");
                CurrentSpawnPoint = DefaultSpawnPoint;
            }

            Respawn();
        }

        public void Save(SaveData saveData)
        {
            if (CurrentSpawnPoint != null)
            {
                saveData.Add(SpawnPointSaveKey, CurrentSpawnPoint.Name);
            }
        }
    }
}
