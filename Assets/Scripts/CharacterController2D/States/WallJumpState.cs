using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class WallJumpState : ICharacterState
    {
        private StateContext StateContext;

        private Vector2 Direction;
        private float WallJumpInputFreezeTimer = 0;
        private float InitialVelocity;
        private float Velocity;

        public WallJumpState(StateContext stateContext)
        {
            StateContext = stateContext;

            InitialVelocity = Mathf.Sqrt(2 * StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.JumpHeight);
        }

        public void Enter()
        {
            CalculateDirection();
            WallJumpInputFreezeTimer = StateContext.CharacterControllerConfig.WallJumpInputFreezeTime;
            Velocity = InitialVelocity;
            StateContext.MovementIntent.Displacement = Vector2.zero;
        }

        public void Exit()
        {
            Velocity = 0;
        }

        public void FixedUpdate()
        {
            if (WallJumpInputFreezeTimer > 0)
            {
                WallJumpInputFreezeTimer -= Time.fixedDeltaTime;

                StateContext.MovementIntent.Displacement = Direction.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
            }
            else
            {
                StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
            }

            StateContext.MovementIntent.Displacement += Vector2.up * Velocity * Time.fixedDeltaTime;

            Velocity -= StateContext.CharacterControllerConfig.Gravity * Time.fixedDeltaTime;

            if (StateContext.DriverSnapshot.JumpReleased)
            {
                Velocity = 0;
            }
        }

        public CharacterStateId? GetTransition()
        {
            if (Velocity <= 0)
            {
                return CharacterStateId.Falling;
            }
            else if (IsTouchingWall(Direction))
            {
                return CharacterStateId.WallSlide;
            }

            return null;
        }

        public void Update()
        {
        }

        private void CalculateDirection()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int rightCount = StateContext.Body.Cast(Vector2.right, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.WallJumpSkinWidth + StateContext.CharacterControllerConfig.SkinWidth);

            Direction = rightCount > 0 ? Vector2.one * Vector2.left : Vector2.one;
        }

        private bool IsTouchingWall(Vector2 direction)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(direction, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
    }
}
