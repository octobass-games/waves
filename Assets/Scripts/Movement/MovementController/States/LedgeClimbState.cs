using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Octobass.Waves.Movement
{
    public class LedgeClimbState : CharacterState
    {
        private readonly MovementConfig Config;
        private readonly MovementControllerCollisionDetector CollisionDetector;

        private Vector2? LedgeClimbTargetPosition;

        public LedgeClimbState(MovementConfig config, MovementControllerCollisionDetector collisionDetector)
        {
            Config = config;
            CollisionDetector = collisionDetector;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            LedgeClimbTargetPosition = CollisionDetector.GetLedgeClimbTargetPosition();
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            if (!CollisionDetector.IsYCoordinateGreaterThanOrEqualTo(LedgeClimbTargetPosition.Value.y))
            {
                return new StateSnapshot()
                {
                    Velocity = Vector2.up * Config.VerticalLedgeClimbSpeed
                };
            }
            else if (!CollisionDetector.IsXCoordinateGreaterThanOrEqualTo(LedgeClimbTargetPosition.Value.x))
            {
                return new StateSnapshot()
                {
                    Velocity = Vector2.right * Config.HorizontalLedgeClimbSpeed
                };
            }

            return new()
            {
                IsLedgeClimbFinished = true
            };
        }
    }
}
