using UnityEngine;

namespace Octobass.Waves.Character
{
    public class PlayerInputCharacterController2DDriver : CharacterController2DDriver
    {
        private PlayerInput PlayerInput;

        private bool JumpPressed;
        private bool JumpReleased;
        private bool GrabPressed;
        private bool GrabReleased;
        private bool AttackPressed;
        private bool AttackReleased;

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

            if (PlayerInput.Movement.Grab.WasPerformedThisFrame())
            {
                GrabPressed = true;
            }

            if (PlayerInput.Movement.Grab.WasReleasedThisFrame())
            {
                GrabReleased = true;
            }

            if (PlayerInput.Movement.Attack.WasPerformedThisFrame())
            {
                AttackPressed = true;
            }

            if (PlayerInput.Movement.Attack.WasReleasedThisFrame())
            {
                AttackReleased = true;
            }

            return new CharacterController2DDriverSnapshot
            {
                Movement = new Vector2(PlayerInput.Movement.Horizontal.ReadValue<float>(), 0),
                Climbing = new Vector2(0, PlayerInput.Movement.Climbing.ReadValue<float>()),
                Swimming = PlayerInput.Movement.Swimming.ReadValue<Vector2>(),
                JumpPressed = JumpPressed,
                JumpReleased = JumpReleased,
                GrabPressed = GrabPressed,
                GrabReleased = GrabReleased,
                GrabHeld = GrabPressed && !GrabReleased,
                AttackPressed = AttackPressed,
                AttackReleased = AttackReleased
            };
        }

        public override void Consume()
        {
            JumpPressed = false;
            JumpReleased = false;

            AttackPressed = false;
            AttackReleased = false;

            if (GrabReleased)
            {
                GrabPressed = false;
                GrabReleased = false;
            }
        }
    }
}
