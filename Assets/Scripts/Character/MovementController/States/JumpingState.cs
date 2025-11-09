using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class JumpingState : CharacterState
    {
        private readonly float Speed;
        private readonly float JumpHeight;
        private readonly float Gravity;
        private readonly float AirMovementSpeedModifier;

        private bool ImpulseApplied;

        public JumpingState(MovementConfig config)
        {
            Speed = config.Speed;
            Gravity = config.Gravity;
            AirMovementSpeedModifier = config.AirMovementSpeedModifier;
            JumpHeight = config.JumpHeight;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            ImpulseApplied = false;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            Vector2 velocity = driverSnapshot.Movement.ProjectX() * AirMovementSpeedModifier * Speed;

            if (!ImpulseApplied)
            {
                velocity.y = Mathf.Sqrt(2 * Gravity * JumpHeight);
                ImpulseApplied = true;
            }
            else if (driverSnapshot.JumpReleased)
            {
                velocity.y = 0;
            }
            else
            {
                velocity.y = previousSnapshot.Velocity.y - Gravity * Time.fixedDeltaTime;
            }

            return new StateSnapshot()
            {
                Velocity = velocity
            };
        }
    }
}
