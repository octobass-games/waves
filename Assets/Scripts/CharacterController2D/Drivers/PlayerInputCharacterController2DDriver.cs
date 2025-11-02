using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class PlayerInputCharacterController2DDriver : CharacterController2DDriver
    {
        public PlayerInput PlayerInput;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();
        }

        public override CharacterController2DDriverSnapshot TakeSnapshot()
        {
            var jumpPressed = PlayerInput.Movement.Jump.ReadValue<float>() > 0.5f;

            Debug.Log($"[PlayerInputCharacterController2DDriver]: Jump pressed - {jumpPressed}");

            return new CharacterController2DDriverSnapshot
            {
                Movement = new Vector2(PlayerInput.Movement.Horizontal.ReadValue<float>(), 0),
                JumpPressed = jumpPressed,
                GrabPressed = PlayerInput.Movement.Grab.ReadValue<float>() == 1f
            };
        }
    }
}
