using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class RidingState : CharacterState
    {
        private readonly MovementConfig Config;
        private readonly MovementControllerCollisionDetector CollisionDetector;

        private IRideable Rideable;

        public RidingState(MovementConfig config, MovementControllerCollisionDetector collisionDetector)
        {
            Config = config;
            CollisionDetector = collisionDetector;
        }

        public override void Enter(CharacterStateId previousStateId, MovementDriverSnapshot _)
        {
            Rideable = CollisionDetector.GetPlatform();
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, MovementDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            return new StateSnapshot()
            {
                Velocity = Rideable.GetVelocity() + new Vector2(driverSnapshot.Movement.x * Config.Speed, 0),
                IsDashAvailable = previousSnapshot.IsDashAvailable
            };
        }
    }
}
