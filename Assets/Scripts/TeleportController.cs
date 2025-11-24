using Octobass.Waves.Character;
using Octobass.Waves.Map;
using Octobass.Waves.Movement;
using Octobass.Waves.Spawn;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField]
    private UnityEngine.InputSystem.PlayerInput PlayerInput;
    // Todo: add action for this
    private InputAction CancelTeleportAction;

    private bool IsTeleporting;

    void Awake()
    {
        if (PlayerInput == null)
        {
            Debug.Log("[TeleportController]: PlayerInput not set");
        }

        CancelTeleportAction = PlayerInput.actions.FindAction("Cancel");
    }

    void Update()
    {
        if (IsTeleporting && CancelTeleportAction.WasPressedThisFrame())
        {
            Finish();
        }
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
