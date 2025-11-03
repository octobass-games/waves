using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class WallJumpState : ICharacterState
    {
        private StateContext StateContext;

        private Vector2 Direction;
        private float WallJumpInputFreezeTimer = 0;
        private float Velocity;
        private bool WallTouched;

        public WallJumpState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            CalculateDirection();
            WallJumpInputFreezeTimer = StateContext.CharacterControllerConfig.WallJumpInputFreezeTime;
            Velocity = Mathf.Sqrt(2 * StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.JumpHeight);
            StateContext.MovementIntent.Displacement = Vector2.zero;
            WallTouched = false;
        }

        public void Exit()
        {
            Velocity = 0;
        }

        public void FixedUpdate()
        {
            if (IsTouchingWall(Direction.ProjectX()))
            {
                WallTouched = true;
            }

            if (WallJumpInputFreezeTimer > 0)
            {
                WallJumpInputFreezeTimer -= Time.fixedDeltaTime;

                if (!WallTouched)
                {
                    StateContext.MovementIntent.Displacement = Direction.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
                }
                else
                {
                    StateContext.MovementIntent.Displacement.x = 0;
                }
            }
            else
            {
                StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
            }

            StateContext.MovementIntent.Displacement.y = (Vector2.up * Velocity * Time.fixedDeltaTime).y;

            Velocity -= StateContext.CharacterControllerConfig.Gravity * Time.fixedDeltaTime;

            if (StateContext.DriverSnapshot.JumpReleased)
            {
                Velocity = 0;
            }
        }

        public CharacterStateId? GetTransition()
        {
            if (Velocity <= 0 || IsTouchingRoof())
            {
                return CharacterStateId.Falling;
            }
            else if (IsTouchingWall(Direction) && StateContext.DriverSnapshot.Movement.x == Direction.x)
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

        private bool IsTouchingRoof()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(Vector2.up, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
    }
}
