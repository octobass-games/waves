using System.Collections.Generic;

namespace Octobass.Waves.Character
{
    public static class MovementStateTransitionRegistry
    {
        public static Dictionary<CharacterStateId, List<MovementStateTransition>> Transitions = new()
        {
            {
                CharacterStateId.Grounded,
                new() {
                    new(CharacterStateId.Jumping, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.JumpPressed),
                    new(CharacterStateId.Riding, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsOnPlatform()),
                    new(CharacterStateId.Falling, (MovementStateMachineContext stateContext) => !stateContext.CharacterController2DCollisionDetector.IsGrounded()),
                    new(CharacterStateId.WallClimb, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingWall() && stateContext.DriverSnapshot.GrabHeld)
                }
            },
            {
                CharacterStateId.Falling,
                new() {
                    new(CharacterStateId.Grounded, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsGrounded()),
                    new(CharacterStateId.Swimming, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingWater()),
                    new(CharacterStateId.Riding, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsOnPlatform()),
                    new(CharacterStateId.WallJump, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsCloseToWall() && stateContext.DriverSnapshot.JumpPressed),
                    new(CharacterStateId.Jumping, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.JumpPressed && stateContext.CoyoteAllowed),
                    new(CharacterStateId.WallClimb, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsCloseToWall() && stateContext.DriverSnapshot.GrabHeld),
                    new(CharacterStateId.WallSlide, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingRightWall() && stateContext.DriverSnapshot.Movement.x > 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingLeftWall() && stateContext.DriverSnapshot.Movement.x < 0)
                }
            },
            {
                CharacterStateId.Jumping,
                new()
                {
                    new(CharacterStateId.Falling, (MovementStateMachineContext stateContext) => stateContext.Velocity <= 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingCeiling()),
                    new(CharacterStateId.WallClimb, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingWall() && stateContext.DriverSnapshot.GrabHeld),
                    new(CharacterStateId.WallJump, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsCloseToWall() && stateContext.DriverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.Riding,
                new()
                {
                    new(CharacterStateId.Falling, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.GetPlatform() == null),
                    new(CharacterStateId.Jumping, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.WallClimb,
                new()
                {
                    new(CharacterStateId.Falling, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.GrabReleased || !stateContext.CharacterController2DCollisionDetector.IsCloseToWall()),
                    new(CharacterStateId.WallJump, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.WallJump,
                new()
                {
                    new(CharacterStateId.Falling, (MovementStateMachineContext stateContext) => stateContext.Velocity <= 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingCeiling()),
                    new(CharacterStateId.WallSlide, (MovementStateMachineContext stateContext) => stateContext.IsTouchingWallForWallJump && stateContext.DriverSnapshot.Movement.x == stateContext.WallJumpDirection.x)
                }
            },
            {
                CharacterStateId.WallSlide,
                new()
                {
                    new(CharacterStateId.Grounded, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsGrounded()),
                    new(CharacterStateId.WallClimb, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.GrabHeld),
                    new(CharacterStateId.WallJump, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.JumpPressed),
                    new(CharacterStateId.Falling, (MovementStateMachineContext stateContext) => stateContext.CharacterController2DCollisionDetector.IsTouchingRightWall() && stateContext.DriverSnapshot.Movement.x <= 0 || stateContext.CharacterController2DCollisionDetector.IsTouchingLeftWall() && stateContext.DriverSnapshot.Movement.x >= 0 || !stateContext.CharacterController2DCollisionDetector.IsTouchingWall())
                }
            },
            {
                CharacterStateId.Swimming,
                new()
                {
                    new(CharacterStateId.Jumping, (MovementStateMachineContext stateContext) => stateContext.DriverSnapshot.JumpPressed)
                }
            }
        };
    }
}
