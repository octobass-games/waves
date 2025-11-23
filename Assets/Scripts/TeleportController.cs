using Octobass.Waves.Map;
using Octobass.Waves.Movement;
using Octobass.Waves.Spawn;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField]
    private MovementController MovementController;

    [SerializeField]
    private MapRenderer MapRenderer;

    [SerializeField]
    private SpawnTracker SpawnTracker;

    [SerializeField]
    private Cartographer Cartographer;

    public void BeginTeleport()
    {
        MovementController.Freeze();
        MapRenderer.ShowTeleportMap();
    }

    public void SetDestination(SpawnPoint spawnPoint)
    {
        SpawnTracker.SetSpawnPoint(spawnPoint);
    }

    public void Teleport()
    {
        MovementController.Unfreeze();
        SpawnTracker.Respawn();
    }
}
