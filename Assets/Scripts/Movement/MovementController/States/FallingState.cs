using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class FallingState : CharacterState
    {
        private readonly MovementConfig Config;

        private float CoyoteTimer;
        private bool IsCoyoteJumpAvailable;

        public FallingState(MovementConfig config)
        {
            Config = config;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            CoyoteTimer = Config.CoyoteTime;
            IsCoyoteJumpAvailable = previousStateId == CharacterStateId.Grounded;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            CoyoteTimer = Mathf.Max(CoyoteTimer - (Time.fixedDeltaTime * 1000), -1);

            return new StateSnapshot()
            {
                Velocity = new Vector2(
                    driverSnapshot.Movement.x * Config.AirMovementSpeedModifier * Config.Speed,
                    Mathf.Max(previousSnapshot.Velocity.y - Config.Gravity * Config.FallingGravityModifier * Time.fixedDeltaTime, -Config.MaxFallSpeed)
                ),
                IsCoyoteJumpAvailable = IsCoyoteJumpAvailable && CoyoteTimer != -1
            };
        }
    }
}
