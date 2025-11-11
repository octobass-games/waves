using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class SwimmingState : CharacterState
    {
        private readonly MovementConfig Config;

        private MovementControllerCollisionDetector CollisionDetector;

        public SwimmingState(MovementConfig config, MovementControllerCollisionDetector collisionDetector)
        {
            Config = config;
            CollisionDetector = collisionDetector;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            Collider2D waterCollider = CollisionDetector.DetectWater();

            var characterY = CollisionDetector.Body.GetComponent<BoxCollider2D>().bounds.max.y;
            var colliderY = waterCollider.bounds.max.y;
            var bobPositionY = colliderY + Config.SwimmingBobHeight;
            float verticalDistanceFromBobHeight = characterY - bobPositionY;

            return new StateSnapshot()
            {
                Velocity = new Vector2(
                    verticalDistanceFromBobHeight == 0 ? driverSnapshot.Movement.x * Config.SwimmingSpeed : 0,
                    verticalDistanceFromBobHeight < 0 ? Mathf.Min(-1, verticalDistanceFromBobHeight / Time.fixedDeltaTime) : Mathf.Max(-1, -(verticalDistanceFromBobHeight / Time.fixedDeltaTime))
                )
            };
        }
    }
}
