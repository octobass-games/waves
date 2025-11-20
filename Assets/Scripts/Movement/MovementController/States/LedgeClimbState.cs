using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Octobass.Waves.Movement
{
    public class LedgeClimbState : CharacterState
    {
        private readonly MovementConfig Config;
        private readonly MovementControllerCollisionDetector CollisionDetector;

        private Vector2? LedgeClimbTargetPosition;
        private Vector2 Direction;

        public LedgeClimbState(MovementConfig config, MovementControllerCollisionDetector collisionDetector)
        {
            Config = config;
            CollisionDetector = collisionDetector;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            LedgeClimbTargetPosition = CollisionDetector.GetLedgeClimbTargetPosition();
            Direction = CollisionDetector.IsTouchingRightWall() ? Vector2.right : Vector2.left;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            if (!CollisionDetector.IsYCoordinateGreaterThanOrEqualTo(LedgeClimbTargetPosition.Value.y))
            {
                return new StateSnapshot()
                {
                    Velocity = Vector2.up * Config.VerticalLedgeClimbSpeed,
                    UnsafeMovement = true
                };
            }
            else if (Direction == Vector2.right && !CollisionDetector.IsXCoordinateGreaterThanOrEqualTo(LedgeClimbTargetPosition.Value.x) || Direction == Vector2.left && !CollisionDetector.IsXCoordinateLessThanOrEqualTo(LedgeClimbTargetPosition.Value.x))
            {
                return new StateSnapshot()
                {
                    Velocity = Direction * Config.HorizontalLedgeClimbSpeed,
                    UnsafeMovement = true
                };
            }

            return new()
            {
                IsLedgeClimbFinished = true
            };
        }
    }
}
