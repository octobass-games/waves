using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class GroundedState : CharacterState
    {
        private readonly float Speed;

        public GroundedState(MovementConfig config)
        {
            Speed = config.Speed;
        }

        public override StateSnapshot Tick(StateSnapshot previousStateSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Movement * Speed
            };
        }
    }
}
