using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class PlayerInputCharacterController2DDriver : CharacterController2DDriver
    {
        private PlayerInput PlayerInput;

        private bool JumpPressed;
        private bool JumpReleased;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();
        }

        public override CharacterController2DDriverSnapshot TakeSnapshot()
        {
            if (PlayerInput.Movement.Jump.WasPerformedThisFrame())
            {
                JumpPressed = true;
            }

            if (PlayerInput.Movement.Jump.WasReleasedThisFrame())
            {
                JumpReleased = true;
            }

            return new CharacterController2DDriverSnapshot
            {
                Movement = new Vector2(PlayerInput.Movement.Horizontal.ReadValue<float>(), 0),
                Climbing = new Vector2(0, PlayerInput.Movement.Climbing.ReadValue<float>()),
                JumpPressed = JumpPressed,
                JumpReleased = JumpReleased,
                GrabPressed = PlayerInput.Movement.Grab.ReadValue<float>() == 1f
            };
        }

        public override void Consume()
        {
            JumpPressed = false;
            JumpReleased = false;
        }
    }
}
