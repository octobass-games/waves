using Octobass.Waves;
using Octobass.Waves.Map;
using Octobass.Waves.Movement;
using Octobass.Waves.Spawn;
using System.Collections.Generic;
using System.Linq;
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
    private Animator Animator;

    [SerializeField]
    private PlayerInput PlayerInput;

    // Todo: add action for this
    private InputAction CancelTeleportAction;

    private List<Teleporter> Teleporters;

    private bool IsTeleporting;

    void Awake()
    {
        if (PlayerInput == null)
        {
            Debug.Log("[TeleportController]: PlayerInput not set");
        }

        if (ServiceLocator.Instance != null)
        {
            ServiceLocator.Instance.Register(this);
        }
        else
        {
            Debug.Log("[TeleportController]: Could not register self with ServiceLocator");
        }

        CancelTeleportAction = PlayerInput.actions.FindAction("Cancel");

        Teleporters = FindObjectsByType<Teleporter>(FindObjectsSortMode.None).ToList();
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

    public void Teleport(RoomId room)
    {
        Teleporter teleport = Teleporters.Find(teleporter => teleporter.GetRoom() == room);

        if (teleport != null)
        {
            SpawnPoint spawnPoint = teleport.GetComponentInChildren<SpawnPoint>();

            if (spawnPoint != null)
            {
                SpawnTracker.SetSpawnPoint(spawnPoint);
                SpawnTracker.Respawn();
            }
        }

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
