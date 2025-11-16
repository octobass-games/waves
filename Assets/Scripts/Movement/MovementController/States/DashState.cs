using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class DashState : CharacterState
    {
        private readonly MovementConfig Config;

        private bool ImpulseApplied;

        public DashState(MovementConfig config)
        {
            Config = config;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            ImpulseApplied = false;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            if (!ImpulseApplied)
            {
                ImpulseApplied = true;

                return new StateSnapshot()
                {
                    Velocity = driverSnapshot.Movement * Mathf.Sqrt(2 * Config.DashDrag * Config.DashDistance)
                };
            }

            Vector2 velocity = previousSnapshot.Velocity - previousSnapshot.Velocity.normalized * Config.DashDrag * Time.fixedDeltaTime;

            velocity.x = previousSnapshot.Velocity.normalized.x == -1 ? Mathf.Min(velocity.x, 0) : Mathf.Max(velocity.x, 0);
            velocity.y = Mathf.Max(velocity.y, 0);

            return new StateSnapshot()
            {
                Velocity = velocity
            };
        }
    }
}
