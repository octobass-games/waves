using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class GroundedState : CharacterState
    {
        private readonly MovementConfig Config;

        public GroundedState(MovementConfig config)
        {
            Config = config;
        }

        public override StateSnapshot Tick(StateSnapshot previousStateSnapshot, CharacterController2DDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Movement.ProjectX() * Config.Speed
            };
        }
    }
}
