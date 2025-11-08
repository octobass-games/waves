using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
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
            StateContext.WallJumpDirection = CalculateDirection();
            WallJumpInputFreezeTimer = StateContext.CharacterControllerConfig.WallJumpInputFreezeTime;
            StateContext.Velocity = Mathf.Sqrt(2 * StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.JumpHeight);
            StateContext.MovementIntent.Displacement = Vector2.zero;
            WallTouched = false;
        }

        public void Exit()
        {
            StateContext.Velocity = 0;
            StateContext.WallJumpDirection = Vector2.zero;
        }

        public void Tick()
        {
            if (IsTouchingWall(StateContext.WallJumpDirection.ProjectX()))
            {
                WallTouched = true;
            }

            if (WallJumpInputFreezeTimer > 0)
            {
                WallJumpInputFreezeTimer -= Time.fixedDeltaTime * 1000;

                if (!WallTouched)
                {
                    StateContext.MovementIntent.Displacement = StateContext.WallJumpDirection.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
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

            StateContext.MovementIntent.Displacement.y = (Vector2.up * StateContext.Velocity * Time.fixedDeltaTime).y;

            StateContext.Velocity -= StateContext.CharacterControllerConfig.Gravity * Time.fixedDeltaTime;
            StateContext.IsTouchingWallForWallJump = IsTouchingWall(StateContext.WallJumpDirection);

            if (StateContext.DriverSnapshot.JumpReleased)
            {
                StateContext.Velocity = 0;
            }
        }

        private Vector2 CalculateDirection()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int rightCount = StateContext.Body.Cast(Vector2.right, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.WallJumpSkinWidth + StateContext.CharacterControllerConfig.SkinWidth);

            return rightCount > 0 ? Vector2.one * Vector2.left : Vector2.one;
        }

        private bool IsTouchingWall(Vector2 direction)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(direction, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
    }
}
