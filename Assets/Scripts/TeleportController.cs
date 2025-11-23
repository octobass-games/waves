using Octobass.Waves.Character;
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

    private PlayerInput PlayerInput;
    private bool IsTeleporting;

    void Awake()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();
    }

    void Update()
    {
        if (IsTeleporting && PlayerInput.Movement.Horizontal.ReadValue<Vector2>().y < 0)
        {
            Finish();
        }
    }

    void OnDisable()
    {
        PlayerInput.Disable();
    }

    public void BeginTeleport()
    {
        IsTeleporting = true;
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
        MapRenderer.ToggleMode();
        IsTeleporting = false;
    }

    private void Finish()
    {
        MovementController.Unfreeze();
        MapRenderer.ToggleMode();
        IsTeleporting = false;
    }
}
