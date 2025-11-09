using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallJumpState : CharacterState
    {
        private readonly float Speed;
        private readonly float WallJumpInputFreezeTime;
        private readonly float Gravity;
        private readonly float JumpHeight;
        private readonly float AirMovementSpeedModifier;

        private MovementControllerCollisionDetector CollisionDetector;
        private Vector2 Direction;
        private float WallJumpInputFreezeTimer = 0;
        private bool WallTouched;
        private bool ImpulseApplied;

        public WallJumpState(MovementConfig config, MovementControllerCollisionDetector collisionDetector)
        {
            Speed = config.Speed;
            WallJumpInputFreezeTime = config.WallJumpInputFreezeTime;
            Gravity = config.Gravity;
            JumpHeight = config.JumpHeight;
            AirMovementSpeedModifier = config.AirMovementSpeedModifier;
            CollisionDetector = collisionDetector;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            Direction = CollisionDetector.IsCloseToRightWall() ? Vector2.one * Vector2.left : Vector2.one;
            WallJumpInputFreezeTimer = WallJumpInputFreezeTime;
            WallTouched = false;
            ImpulseApplied = false;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            Vector2 velocity;

            if (!ImpulseApplied)
            {
                velocity.y = Mathf.Sqrt(2 * Gravity * JumpHeight);
                ImpulseApplied = true;
            }
            else if (driverSnapshot.JumpReleased)
            {
                velocity.y = 0;
            }
            else
            {
                velocity.y = previousSnapshot.Velocity.y - Gravity * Time.fixedDeltaTime;
            }

            if (Direction.x == -1 && CollisionDetector.IsTouchingLeftWall() || Direction.x == 1 && CollisionDetector.IsTouchingRightWall())
            {
                WallTouched = true;
            }

            if (WallJumpInputFreezeTimer > 0)
            {
                WallJumpInputFreezeTimer -= Time.fixedDeltaTime * 1000;

                if (!WallTouched)
                {
                    velocity.x = Direction.ProjectX().x * AirMovementSpeedModifier * Speed;
                }
                else
                {
                    velocity.x = 0;
                }
            }
            else
            {
                velocity.x = driverSnapshot.Movement.ProjectX().x * AirMovementSpeedModifier * Speed;
            }

            return new StateSnapshot()
            {
                Velocity = velocity,
                Direction = Direction
            };
        }
    }
}
