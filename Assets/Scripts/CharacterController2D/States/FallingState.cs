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
            if (StateContext.CharacterController2DCollisionDetector.IsGrounded())
            {
                return CharacterStateId.Grounded;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsOnPlatform())
            {
                return CharacterStateId.Riding;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsCloseToWall() && StateContext.DriverSnapshot.JumpPressed)
            {
                return CharacterStateId.WallJump;
            }
            else if (CoyoteTimer > 0 && StateContext.DriverSnapshot.JumpPressed && !StateContext.JumpConsumed)
            {
                return CharacterStateId.Jumping;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsCloseToWall() && StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.WallClimb;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsTouchingRightWall() && StateContext.DriverSnapshot.Movement.x > 0 || StateContext.CharacterController2DCollisionDetector.IsTouchingLeftWall() && StateContext.DriverSnapshot.Movement.x < 0)
            {
                return CharacterStateId.WallSlide;
            }

            return null;
        }

        public void Update()
        {
        }
    }
}
