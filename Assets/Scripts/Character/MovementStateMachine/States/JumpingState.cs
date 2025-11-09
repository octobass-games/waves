using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class JumpingState : CharacterState
    {
        private MovementStateMachineContext StateContext;

        private float Velocity;

        public JumpingState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public override void Enter()
        {
            StateContext.Velocity = Mathf.Sqrt(2 * StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.JumpHeight);
            StateContext.CoyoteAllowed = false;
        }

        public override void Exit()
        {
            StateContext.Velocity = 0;
        }

        public override void Tick()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime + Vector2.up * StateContext.Velocity * Time.fixedDeltaTime;

            StateContext.Velocity -= StateContext.CharacterControllerConfig.Gravity * Time.fixedDeltaTime;

            if (StateContext.DriverSnapshot.JumpReleased)
            {
                StateContext.Velocity = 0;
            }
        }
    }
}
