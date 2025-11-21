using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class WallClimbState : CharacterState
    {
        private readonly MovementConfig Config;

        public WallClimbState(MovementConfig config)
        {
            Config = config;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Climbing * Config.WallClimbSpeed,
                IsDashAvailable = previousSnapshot.IsDashAvailable
            };
        }
    }
}
