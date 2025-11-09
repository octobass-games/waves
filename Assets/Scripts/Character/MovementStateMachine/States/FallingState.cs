using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class FallingState : CharacterState
    {
        private MovementStateMachineContext StateContext;
        private float Velocity;
        private float CoyoteTimer;

        public FallingState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public override void Enter()
        {
            Velocity = 0;
            CoyoteTimer = StateContext.CharacterControllerConfig.CoyoteTime;
        }

        public override void Exit()
        {
            Velocity = 0;
            CoyoteTimer = StateContext.CharacterControllerConfig.CoyoteTime;
            StateContext.CoyoteAllowed = false;
        }

        public override void Tick()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime + Vector2.up * Velocity * Time.fixedDeltaTime;

            Velocity = Mathf.Max(Velocity - StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.FallingGravityModifier * Time.fixedDeltaTime, -StateContext.CharacterControllerConfig.MaxFallSpeed);
            CoyoteTimer = Mathf.Max(CoyoteTimer - (Time.fixedDeltaTime * 1000), -1);
            
            if (CoyoteTimer == -1)
            {
                StateContext.CoyoteAllowed = false;
            }
        }
    }
}
