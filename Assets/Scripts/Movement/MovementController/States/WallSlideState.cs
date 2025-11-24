using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class WallSlideState : CharacterState
    {
        private readonly MovementConfig Config;

        public WallSlideState(MovementConfig config)
        {
            Config = config;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, MovementDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            return new StateSnapshot()
            {
                Velocity = Vector2.down * Config.WallSlideSpeed,
                IsDashAvailable = previousSnapshot.IsDashAvailable
            };
        }
    }
}
