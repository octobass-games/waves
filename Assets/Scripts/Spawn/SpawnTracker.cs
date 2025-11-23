using Octobass.Waves.Camera;
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
        private CameraSwitcher CameraSwitcher;

        [SerializeField]
        private List<SpawnPointRoomBinding> SpawnPointRoomBindings;

        private SpawnPointRoomBinding CurrentSpawnPointRoomBinding;

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
            if (CurrentSpawnPointRoomBinding != null)
            {
                Vector2 bottomOfSpawnPoint = new(CurrentSpawnPointRoomBinding.SpawnPoint.transform.position.x, CurrentSpawnPointRoomBinding.SpawnPoint.GetComponent<BoxCollider2D>().bounds.min.y);
                MovementController.ResetAtPosition(bottomOfSpawnPoint);
                CameraSwitcher.OnRoomEntered(CurrentSpawnPointRoomBinding.Room);
            }
            else
            {
                Debug.LogWarning("[SpawnTracker]: Does not have CurrentSpawnPoint set");
            }
        }

        public void SetSpawnPoint(SpawnPoint spawnPoint)
        {
            SpawnPointRoomBinding spawnPointRoomBinding = SpawnPointRoomBindings.Find(spawnPointRoomBinding => spawnPointRoomBinding.SpawnPoint == spawnPoint);

            if (spawnPointRoomBinding != null)
            {
                CurrentSpawnPointRoomBinding = spawnPointRoomBinding;
            }
            else
            {
                Debug.Log($"[SpawnTracker]: Could not find binding for SpawnPoint - {spawnPoint.name}");
            }
        }

        public void Load(SaveData saveData)
        {
            string spawnPointName = saveData.Load<string>(SpawnPointSaveKey);
            SpawnPointRoomBinding spawnPointRoomBinding = SpawnPointRoomBindings.Find(spawnPointRoomBinding => spawnPointRoomBinding.SpawnPoint.Name == spawnPointName);

            if (spawnPointRoomBinding != null)
            {
                CurrentSpawnPointRoomBinding = spawnPointRoomBinding;
            }
            else
            {
                Debug.Log($"[SpawnTracker]: SpawnPoint not found with name - {spawnPointName}");
                CurrentSpawnPointRoomBinding = SpawnPointRoomBindings[0];
            }

            Respawn();
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(SpawnPointSaveKey, CurrentSpawnPointRoomBinding.SpawnPoint.Name);
        }
    }
}
