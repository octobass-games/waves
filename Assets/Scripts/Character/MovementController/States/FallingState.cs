using UnityEngine;

namespace Octobass.Waves.Character
{
    public class FallingState : CharacterState
    {
        private readonly float Speed;
        private readonly float Gravity;
        private readonly float FallingGravityModifier;
        private readonly float AirMovementSpeedModifier;
        private readonly float CoyoteTime;
        private readonly float MaxFallSpeed;

        private float CoyoteTimer;
        private bool IsCoyoteJumpAvailable;

        public FallingState(MovementConfig config)
        {
            Speed = config.Speed;
            Gravity = config.Gravity;
            MaxFallSpeed = config.MaxFallSpeed;
            FallingGravityModifier = config.FallingGravityModifier;
            AirMovementSpeedModifier = config.AirMovementSpeedModifier;
            CoyoteTime = config.CoyoteTime;
        }

        public override void Enter(CharacterStateId previousStateId)
        {
            CoyoteTimer = CoyoteTime;
            IsCoyoteJumpAvailable = previousStateId == CharacterStateId.Grounded;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            CoyoteTimer = Mathf.Max(CoyoteTimer - (Time.fixedDeltaTime * 1000), -1);

            return new StateSnapshot()
            {
                Velocity = new Vector2(
                    driverSnapshot.Movement.x * AirMovementSpeedModifier * Speed,
                    Mathf.Max(previousSnapshot.Velocity.y - Gravity * FallingGravityModifier * Time.fixedDeltaTime, -MaxFallSpeed)
                ),
                IsCoyoteJumpAvailable = IsCoyoteJumpAvailable && CoyoteTimer != -1
            };
        }
    }
}
