using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class DivingState : CharacterState
    {
        private readonly float Speed;
        private readonly float DivingSpeedModifier;

        public DivingState(MovementConfig config)
        {
            Speed = config.Speed;
            DivingSpeedModifier = config.DivingSpeedModifier;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Swimming * DivingSpeedModifier * Speed
            };
        }
    }
}
