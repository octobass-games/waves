using NUnit.Framework.Constraints;
using Octobass.Waves.Extensions;
using Unity.Cinemachine;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class SwimmingState : CharacterState
    {
        private readonly MovementConfig Config;
        private readonly MovementControllerCollisionDetector CollisionDetector;

        private bool IsRising;

        public SwimmingState(MovementConfig config, MovementControllerCollisionDetector collisionDetector)
        {
            Config = config;
            CollisionDetector = collisionDetector;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            IsRising = previousStateId == CharacterStateId.Diving;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            Collider2D waterCollider = CollisionDetector.DetectWaterway();

            var characterY = CollisionDetector.Body.GetComponent<BoxCollider2D>().bounds.max.y;
            var colliderY = waterCollider.bounds.max.y;
            var bobPositionY = colliderY + Config.SwimmingBobHeight;
            float verticalDistanceFromBobHeight = characterY - bobPositionY;

            Vector2 velocity = previousSnapshot.Velocity;

            velocity.x = verticalDistanceFromBobHeight == 0 ? driverSnapshot.Movement.x * Config.SwimmingSpeed : 0;
            velocity.y = velocity.y + Config.Gravity * Config.BuoyancyDescentModifier * Time.fixedDeltaTime;

            if (velocity.y >= 0)
            {
                IsRising = true;
            }

            return new StateSnapshot()
            {
                Velocity = new Vector2(
                    velocity.x,
                    IsRising ? (verticalDistanceFromBobHeight > 0 ? -(verticalDistanceFromBobHeight / Time.fixedDeltaTime) : (verticalDistanceFromBobHeight == 0) ? 0 : Config.BuoyancyAscentSpeed) : velocity.y
                )
            };
        }
    }
}
