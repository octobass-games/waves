using UnityEngine;
using UnityEngine.InputSystem;

namespace Octobass.Waves.CharacterController2D
{
    public class PlayerInputCharacterController2DDriver : CharacterController2DDriver
    {
        // public PlayerInputActions PlayerInputActions;

        void Awake()
        {
            // PlayerInputActions = new PlayerInputActions();
            // PlayerInputActions.Enable();
        }

        public override CharacterController2DDriverSnapshot TakeSnapshot()
        {
            return new CharacterController2DDriverSnapshot();
        }
    }
}
