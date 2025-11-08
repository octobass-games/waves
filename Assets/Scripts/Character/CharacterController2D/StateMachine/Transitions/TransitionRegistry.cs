using System.Collections.Generic;

namespace Octobass.Waves.Character
{
    public static class TransitionRegistry
    {
        public static Dictionary<CharacterStateId, List<Transition>> Transitions = new()
        {
            {
                CharacterStateId.Grounded,
                new() {
                    new(CharacterStateId.Jumping, (StateContext stateContext) => stateContext.DriverSnapshot.JumpPressed),
                    new(CharacterStateId.Riding, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsOnPlatform()),
                    new(CharacterStateId.Falling, (StateContext stateContext) => !stateContext.CharacterController2DCollisionDetector.IsGrounded()),
                    new(CharacterStateId.WallClimb, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingWall() && stateContext.DriverSnapshot.GrabHeld)
                }
            },
            {
                CharacterStateId.Falling,
                new() {
                    new(CharacterStateId.Grounded, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsGrounded()),
                    new(CharacterStateId.Riding, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsOnPlatform()),
                    new(CharacterStateId.WallJump, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsCloseToWall() && stateContext.DriverSnapshot.JumpPressed),
                    new(CharacterStateId.Jumping, (StateContext stateContext) => stateContext.DriverSnapshot.JumpPressed && stateContext.CoyoteAllowed),
                    new(CharacterStateId.WallClimb, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsCloseToWall() && stateContext.DriverSnapshot.GrabHeld),
                    new(CharacterStateId.WallSlide, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingRightWall() && stateContext.DriverSnapshot.Movement.x > 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingLeftWall() && stateContext.DriverSnapshot.Movement.x < 0)
                }
            },
            {
                CharacterStateId.Jumping,
                new()
                {
                    new(CharacterStateId.Falling, (StateContext stateContext) => stateContext.Velocity <= 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingCeiling()),
                    new(CharacterStateId.WallClimb, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingWall() && stateContext.DriverSnapshot.GrabHeld),
                    new(CharacterStateId.WallJump, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsCloseToWall() && stateContext.DriverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.Riding,
                new()
                {
                    new(CharacterStateId.Falling, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.GetPlatform() == null),
                    new(CharacterStateId.Jumping, (StateContext stateContext) => stateContext.DriverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.WallClimb,
                new()
                {
                    new(CharacterStateId.Falling, (StateContext stateContext) => stateContext.DriverSnapshot.GrabReleased || !stateContext.CharacterController2DCollisionDetector.IsTouchingWall()),
                    new(CharacterStateId.WallJump, (StateContext stateContext) => stateContext.DriverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.WallJump,
                new()
                {
                    new(CharacterStateId.Falling, (StateContext stateContext) => stateContext.Velocity <= 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingCeiling()),
                    new(CharacterStateId.WallSlide, (StateContext stateContext) => stateContext.IsTouchingWallForWallJump && stateContext.DriverSnapshot.Movement.x == stateContext.WallJumpDirection.x)
                }
            },
            {
                CharacterStateId.WallSlide,
                new()
                {
                    new(CharacterStateId.Grounded, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsGrounded()),
                    new(CharacterStateId.WallClimb, (StateContext stateContext) => stateContext.DriverSnapshot.GrabHeld),
                    new(CharacterStateId.WallJump, (StateContext stateContext) => stateContext.DriverSnapshot.JumpPressed),
                    new(CharacterStateId.Falling, (StateContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingRightWall() && stateContext.DriverSnapshot.Movement.x <= 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingLeftWall() && stateContext.DriverSnapshot.Movement.x >= 0 || !stateContext.CharacterController2DCollisionDetector.IsTouchingWall())
                }
            }
        };
    }
}
