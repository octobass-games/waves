using Octobass.Waves.Character;
using Octobass.Waves.Movement;
using Octobass.Waves.Save;
using Octobass.Waves.Spawn;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Octobass.Waves.Debugging
{
    public class DebugMenu : MonoBehaviour
    {
        public MovementController PlayerMovementController;
        public bool UnlockAllAbilitiesOnAwake = true;
        public GameObject DebugMenuUi;
        public SpawnTracker SpawnTracker;
        public SaveManager SaveManager;

        [SerializeField]
        private UnityEngine.InputSystem.PlayerInput PlayerInput;

        private InputAction RightClickAction;

        void Awake()
        {
            if (PlayerInput == null)
            {
                Debug.LogWarning("[DebugMenu]: PlayerInput not set");
            }

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

            if (SaveManager == null)
            {
                Debug.LogWarning("[DebugMenu]: SaveManager not set");
            }

            RightClickAction = PlayerInput.actions.FindAction("RightClick");
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
                AddState("Dashing");
            }
        }

        void Update()
        {
            if (RightClickAction.WasPressedThisFrame())
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

        public void Save()
        {
            SaveManager.Save();
        }

        public void Load()
        {
            SaveManager.Load();
        }
    }
}
