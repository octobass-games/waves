using Octobass.Waves.Extensions;
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

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, MovementDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Movement.ProjectY() * Config.WallClimbSpeed,
                IsDashAvailable = previousSnapshot.IsDashAvailable
            };
        }
    }
}
