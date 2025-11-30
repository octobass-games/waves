using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class DashState : CharacterState
    {
        private readonly MovementConfig Config;

        private bool ImpulseApplied;
        private float InputGracePeriodTimer;
        private bool InputGracePeriodFinished;

        public DashState(MovementConfig config)
        {
            Config = config;
        }

        public override void Enter(CharacterStateId previousStateId, MovementDriverSnapshot _)
        {
            ImpulseApplied = false;
            InputGracePeriodFinished = false;
            InputGracePeriodTimer = Config.DashGracePeriod;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, MovementDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            if (!InputGracePeriodFinished)
            {
                InputGracePeriodTimer -= Time.fixedDeltaTime * 1000;

                if (InputGracePeriodTimer < 0)
                {
                    InputGracePeriodFinished = true;
                }
                else
                {
                    return new StateSnapshot()
                    {
                        Velocity = Vector2.zero,
                        IsDashAvailable = false,
                        IsDashGracePeriodFinished = false
                    };
                }
            }

            if (!ImpulseApplied && InputGracePeriodFinished)
            {
                ImpulseApplied = true;

                Debug.Log(driverSnapshot.Movement.normalized);

                return new StateSnapshot()
                {
                    Velocity = (driverSnapshot.Movement == Vector2.zero ? facingDirection : driverSnapshot.Movement.normalized) * Mathf.Sqrt(2 * Config.DashDrag * Config.DashDistance),
                    IsDashAvailable = false,
                    IsDashGracePeriodFinished = true
                };
            }

            Vector2 velocity = previousSnapshot.Velocity - previousSnapshot.Velocity.normalized * Config.DashDrag * Time.fixedDeltaTime;

            velocity.x = previousSnapshot.Velocity.normalized.x < 0 ? Mathf.Min(velocity.x, 0) : Mathf.Max(velocity.x, 0);
            velocity.y = previousSnapshot.Velocity.normalized.y < 0 ? Mathf.Min(velocity.y, 0) : Mathf.Max(velocity.y, 0);

            return new StateSnapshot()
            {
                Velocity = velocity,
                IsDashAvailable = false,
                IsDashGracePeriodFinished = true
            };
        }
    }
}
