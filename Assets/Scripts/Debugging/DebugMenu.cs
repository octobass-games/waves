using Octobass.Waves.Character;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Octobass.Waves.Debugging
{
    public class DebugMenu : MonoBehaviour
    {
        public CharacterController2D Player;
        public GameObject DebugMenuUi;

        private PlayerInput PlayerInput;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();

            if (DebugMenuUi == null)
            {
                Debug.LogWarning("[DebugMenu]: DebugMenuUi not set");
            }

            if (Player == null)
            {
                Debug.LogWarning("[DebugMenu]: Player not set");
            }
        }

        void Update()
        {
            if (PlayerInput.UI.RightClick.WasPressedThisFrame())
            {
                DebugMenuUi.SetActive(!DebugMenuUi.activeSelf);
            }

        }

        public void AddState(string state)
        {
            if (Enum.TryParse<CharacterStateId>(state, out CharacterStateId stateId))
            {
                Player.AddState(stateId);
            }
            else
            {
                Debug.LogWarning($"[DebugMenu]: Could not parse string to CharacterStateId - {state}");
            }
        }
    }
}
