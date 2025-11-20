using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public static class MovementStateTransitionRegistry
    {
        public static Dictionary<CharacterStateId, List<MovementStateTransition>> Transitions = new()
        {
            {
                CharacterStateId.Grounded,
                new() {
                    new(CharacterStateId.Jumping, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.JumpPressed),
                    new(CharacterStateId.Dashing, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.DashPressed),
                    new(CharacterStateId.Riding, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsOnPlatform()),
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => !collisionDetector.IsGrounded()),
                    new(CharacterStateId.WallClimb, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsTouchingWall() && driverSnapshot.GrabHeld)
                }
            },
            {
                CharacterStateId.Falling,
                new() {
                    new(CharacterStateId.Dashing, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.DashPressed),
                    new(CharacterStateId.Grounded, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsGrounded()),
                    new(CharacterStateId.Swimming, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsTouchingWaterway()),
                    new(CharacterStateId.Riding, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsOnPlatform()),
                    new(CharacterStateId.WallJump, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsCloseToWall() && driverSnapshot.JumpPressed),
                    new(CharacterStateId.Jumping, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.JumpPressed && stateSnapshot.IsCoyoteJumpAvailable),
                    new(CharacterStateId.WallClimb, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsCloseToWall() && driverSnapshot.GrabHeld),
                    new(CharacterStateId.WallSlide, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsTouchingRightWall() && driverSnapshot.Movement.x > 0 || collisionDetector.IsTouchingLeftWall() && driverSnapshot.Movement.x < 0)
                }
            },
            {
                CharacterStateId.Jumping,
                new()
                {
                    new(CharacterStateId.Dashing, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.DashPressed),
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => stateSnapshot.Velocity.y <= 0 || collisionDetector.IsTouchingCeiling()),
                    new(CharacterStateId.WallClimb, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsTouchingWall() && driverSnapshot.GrabHeld),
                    new(CharacterStateId.WallJump, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsCloseToWall() && driverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.Riding,
                new()
                {
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.GetPlatform() == null),
                    new(CharacterStateId.Jumping, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.JumpPressed)
                }
            },
            {
                CharacterStateId.WallClimb,
                new()
                {
                    new(CharacterStateId.WallSlide, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.GrabReleased && (collisionDetector.IsTouchingRightWall() && driverSnapshot.Movement.x > 0 || collisionDetector.IsTouchingLeftWall() && driverSnapshot.Movement.x < 0)),
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.GrabReleased || !collisionDetector.IsCloseToWall()),
                    new(CharacterStateId.WallJump, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.JumpPressed),
                    new(CharacterStateId.Dashing, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.DashPressed),
                    new(CharacterStateId.LedgeClimb, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsAtLedge())
                }
            },
            {
                CharacterStateId.WallJump,
                new()
                {
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => stateSnapshot.Velocity.y <= 0 || collisionDetector.IsTouchingCeiling()),
                    new(CharacterStateId.WallSlide, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsTouchingWall(stateSnapshot.Direction) && driverSnapshot.Movement.x == stateSnapshot.Direction.x),
                    new(CharacterStateId.Dashing, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.DashPressed),
                }
            },
            {
                CharacterStateId.WallSlide,
                new()
                {
                    new(CharacterStateId.Grounded, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsGrounded()),
                    new(CharacterStateId.WallClimb, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.GrabHeld),
                    new(CharacterStateId.WallJump, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.JumpPressed),
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsTouchingRightWall() && driverSnapshot.Movement.x <= 0 || collisionDetector.IsTouchingLeftWall() && driverSnapshot.Movement.x >= 0 || !collisionDetector.IsTouchingWall()),
                    new(CharacterStateId.Dashing, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.DashPressed),
                }
            },
            {
                CharacterStateId.Swimming,
                new()
                {
                    new(CharacterStateId.Jumping, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.JumpPressed),
                    new(CharacterStateId.Diving, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => driverSnapshot.Swimming.y < 0)
                }
            },
            {
                CharacterStateId.Diving,
                new()
                {
                    new(CharacterStateId.Swimming,  (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => collisionDetector.IsSwimmingAtWaterwayEntrance())
                }
            },
            {
                CharacterStateId.Dashing,
                new()
                {
                    new(CharacterStateId.WallClimb, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => !collisionDetector.IsGrounded() && collisionDetector.IsTouchingWall(stateSnapshot.Velocity.normalized) && driverSnapshot.GrabPressed),
                    new(CharacterStateId.WallSlide, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => !collisionDetector.IsGrounded() && collisionDetector.IsTouchingWall(stateSnapshot.Velocity.normalized) && driverSnapshot.Movement.normalized.x == stateSnapshot.Velocity.normalized.x),
                    new(CharacterStateId.Falling, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => stateSnapshot.Velocity == Vector2.zero && !collisionDetector.IsGrounded()),
                    new(CharacterStateId.Grounded, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => stateSnapshot.Velocity == Vector2.zero && collisionDetector.IsGrounded()),
                }
            },
            {
                CharacterStateId.LedgeClimb,
                new()
                {
                    new(CharacterStateId.Grounded, (StateSnapshot stateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, MovementControllerCollisionDetector collisionDetector) => stateSnapshot.IsLedgeClimbFinished)
                }
            }
        };
    }
}
