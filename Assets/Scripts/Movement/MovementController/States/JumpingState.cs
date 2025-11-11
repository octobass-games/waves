using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class JumpingState : CharacterState
    {
        private readonly MovementConfig Config;

        private bool ImpulseApplied;

        public JumpingState(MovementConfig config)
        {
            Config = config;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            ImpulseApplied = false;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            Vector2 velocity = driverSnapshot.Movement.ProjectX() * Config.AirMovementSpeedModifier * Config.Speed;

            if (!ImpulseApplied)
            {
                velocity.y = Mathf.Sqrt(2 * Config.Gravity * Config.JumpHeight);
                ImpulseApplied = true;
            }
            else if (driverSnapshot.JumpReleased)
            {
                velocity.y = 0;
            }
            else
            {
                velocity.y = previousSnapshot.Velocity.y - Config.Gravity * Time.fixedDeltaTime;
            }

            return new StateSnapshot()
            {
                Velocity = velocity
            };
        }
    }
}
