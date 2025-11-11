using Octobass.Waves.Movement;
using Octobass.Waves.Spawn;
using System;
using UnityEngine;

namespace Octobass.Waves.Debugging
{
    public class DebugMenu : MonoBehaviour
    {
        public MovementController PlayerMovementController;
        public bool UnlockAllAbilitiesOnAwake = true;
        public GameObject DebugMenuUi;
        public SpawnTracker SpawnTracker;

        private PlayerInput PlayerInput;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            if (DebugMenuUi == null)
            {
                Debug.LogWarning("[DebugMenu]: DebugMenuUi not set");
            }

            if (PlayerMovementController == null)
            {
                Debug.LogWarning("[DebugMenu]: Player not set");
            }

            if (SpawnTracker == null)
            {
                Debug.LogWarning("[DebugMenu]: SpawnTracker not set");
            }
        }

        void Start()
        {
            if (UnlockAllAbilitiesOnAwake)
            {
                AddState("Jumping");
                AddState("WallClimb");
                AddState("WallJump");
                AddState("Swimming");
                AddState("Diving");
            }
        }

        void Update()
        {
            if (PlayerInput.UI.RightClick.WasPressedThisFrame())
            {
                DebugMenuUi.SetActive(!DebugMenuUi.activeSelf);
            }

        }

        public void Respawn()
        {
            SpawnTracker.Respawn();
        }

        public void AddState(string state)
        {
            if (Enum.TryParse(state, out CharacterStateId stateId))
            {
                PlayerMovementController.AddState(stateId);
            }
            else
            {
                Debug.LogWarning($"[DebugMenu]: Could not parse string to CharacterStateId - {state}");
            }
        }
    }
}
