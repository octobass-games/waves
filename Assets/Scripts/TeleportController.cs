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

    private InputAction CancelTeleportAction;

    private List<Teleporter> Teleporters;

    void Start()
    {
        if (PlayerInput == null)
        {
            Debug.Log("[TeleportController]: PlayerInput not set");
        }

        CancelTeleportAction = PlayerInput.actions.FindAction("Cancel");

        Teleporters = FindObjectsByType<Teleporter>(FindObjectsSortMode.None).ToList();
    }

    public void BeginTeleport()
    {
        MapRenderer.ShowMiniMap();
        PlayerInput.SwitchCurrentActionMap("UI");

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
        Finish();
    }

    public void Finish()
    {
        MapRenderer.ShowMiniMap();
        Animator.SetTrigger("IsTeleportingFinished");

        PlayerInput.SwitchCurrentActionMap("Gameplay");
    }
}
