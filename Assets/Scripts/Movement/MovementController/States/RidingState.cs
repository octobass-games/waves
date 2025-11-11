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

        public override void Enter(CharacterStateId previousStateId)
        {
            Rideable = CollisionDetector.GetPlatform();
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = Rideable.GetVelocity() + new Vector2(driverSnapshot.Movement.x * Config.Speed, 0)
            };
        }
    }
}
