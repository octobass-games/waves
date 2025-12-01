using UnityEngine;
using UnityEngine.InputSystem;

namespace Octobass.Waves.Movement
{
    public class MovementDriver : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput PlayerInput;

        private InputAction JumpAction;
        private InputAction GrabAction;
        private InputAction AttackAction;
        private InputAction DashAction;
        private InputAction HorizontalAction;
        private InputAction ClimbingAction;
        private InputAction SwimmingAction;

        private float JumpInputBuffer;

        private bool JumpPressed;
        private bool JumpReleased;
        private bool DashPressed;
        private bool DashReleased;
        private bool GrabPressed;
        private bool GrabReleased;
        private bool AttackPressed;
        private bool AttackReleased;

        void Start()
        {
            if (PlayerInput == null)
            {
                Debug.Log("[PlayerInputCharacterController2DDriver]: PlayerInput not set");
            }

            JumpAction = PlayerInput.actions.FindAction("Jump");
            GrabAction = PlayerInput.actions.FindAction("Grab");
            AttackAction = PlayerInput.actions.FindAction("Attack");
            DashAction = PlayerInput.actions.FindAction("Dash");
            HorizontalAction = PlayerInput.actions.FindAction("Horizontal");
            ClimbingAction = PlayerInput.actions.FindAction("Climbing");
            SwimmingAction = PlayerInput.actions.FindAction("Swimming");
        }

        void Update()
        {
            JumpInputBuffer -= Time.deltaTime * 1000;

            if (JumpInputBuffer <= 0)
            {
                JumpPressed = false;
            }
        }

        public MovementDriverSnapshot TakeSnapshot()
        {
            if (JumpAction.WasPerformedThisFrame())
            {
                JumpPressed = true;
                JumpInputBuffer = 100;
            }

            if (JumpAction.WasReleasedThisFrame())
            {
                JumpReleased = true;
                JumpInputBuffer = 0;
            }

            if (GrabAction.WasPerformedThisFrame())
            {
                GrabPressed = true;
            }

            if (GrabAction.WasReleasedThisFrame() || !GrabAction.enabled)
            {
                GrabReleased = true;
            }

            if (AttackAction.WasPerformedThisFrame())
            {
                AttackPressed = true;
            }

            if (AttackAction.WasReleasedThisFrame())
            {
                AttackReleased = true;
            }

            if (DashAction.WasPerformedThisFrame())
            {
                DashPressed = true;
            }

            if (DashAction.WasReleasedThisFrame())
            {
                DashReleased = true;
            }


            return new MovementDriverSnapshot
            {
                Movement = HorizontalAction.ReadValue<Vector2>(),
                DashPressed = DashPressed,
                DashReleased = DashReleased,
                Swimming = SwimmingAction.ReadValue<Vector2>(),
                JumpPressed = JumpPressed,
                JumpReleased = JumpReleased,
                GrabPressed = GrabPressed,
                GrabReleased = GrabReleased,
                GrabHeld = GrabPressed && !GrabReleased,
                AttackPressed = AttackPressed,
                AttackReleased = AttackReleased,
            };
        }

        public void Consume(MovementDriverSnapshot movementDriverSnapshot)
        {
            if (JumpInputBuffer <= 0 || movementDriverSnapshot.JumpConsumed)
            {
                JumpPressed = false;
            }
             
            if (JumpReleased)
            {
                JumpPressed = false;
                JumpReleased = false;
            }

            DashPressed = false;
            DashReleased = false;

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
