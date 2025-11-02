using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class FallingState : ICharacterState
    {
        private StateContext StateContext;
        private float Velocity;
        private float CoyoteTimer;

        public FallingState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            Velocity = 0;
            CoyoteTimer = StateContext.CharacterControllerConfig.CoyoteTime;
        }

        public void Exit()
        {
            Velocity = 0;
            CoyoteTimer = StateContext.CharacterControllerConfig.CoyoteTime;
        }

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime + Vector2.up * Velocity * Time.fixedDeltaTime;

            Velocity = Mathf.Max(Velocity - StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.FallingGravityModifier * Time.fixedDeltaTime, -StateContext.CharacterControllerConfig.MaxFallSpeed);
            CoyoteTimer = Mathf.Max(CoyoteTimer - (Time.fixedDeltaTime * 1000), -1);
        }

        public CharacterStateId? GetTransition()
        {
            if (IsGrounded())
            {
                return CharacterStateId.Grounded;
            }
            else if (IsLandingOnMovingPlatform())
            {
                return CharacterStateId.Riding;
            }
            else if (CoyoteTimer > 0 && StateContext.DriverSnapshot.Movement.y > 0 && !StateContext.JumpConsumed)
            {
                return CharacterStateId.Jumping;
            }
            else if (IsTouchingWall() && StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.WallClimb;
            }
            else if (IsTouchingWall(Vector2.right) && StateContext.DriverSnapshot.Movement.x > 0 || IsTouchingWall(Vector2.left) && StateContext.DriverSnapshot.Movement.x < 0)
            {
                return CharacterStateId.WallSlide;
            }

            return null;
        }

        public void Update()
        {
        }

        private bool IsGrounded()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(Vector2.down, StateContext.CharacterControllerConfig.CollisionContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
        private bool IsLandingOnMovingPlatform()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(Vector2.down, StateContext.CharacterControllerConfig.RideableContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }

        private bool IsTouchingWall()
        {
            return IsTouchingWall(Vector2.right) || IsTouchingWall(Vector2.left);
        }

        private bool IsTouchingWall(Vector2 direction)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(direction, StateContext.CharacterControllerConfig.CollisionContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
    }
}
