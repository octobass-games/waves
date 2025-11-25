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
    private Animator Animator;

    [SerializeField]
    private PlayerInput PlayerInput;
    
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
        
        PlayerInput.actions.FindActionMap("Gameplay").Disable();
        PlayerInput.actions.FindActionMap("UI").Enable();

        Animator.SetTrigger("IsEnteringTeleporter");
    }

    void OnTeleporterAnimationEnd()
    {
        MapRenderer.ShowTeleportMap();
    }

    public void Teleport(SpawnPoint teleportPoint)
    {
        SpawnTracker.SetSpawnPoint(teleportPoint);
        SpawnTracker.Respawn();
        MapRenderer.ToggleMode();
        IsTeleporting = false;
        Finish();
    }

    private void Finish()
    {
        MapRenderer.ToggleMode();
        Animator.SetTrigger("IsTeleportingFinished");

        PlayerInput.actions.FindActionMap("Gameplay").Enable();
        PlayerInput.actions.FindActionMap("UI").Disable();
        
        IsTeleporting = false;
    }
}
