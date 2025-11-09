using UnityEngine;

namespace Octobass.Waves.Character
{
    public class GroundedState : CharacterState
    {
        private MovementStateMachineContext StateContext;

        public GroundedState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public override void Tick()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
            StateContext.CoyoteAllowed = true;
        }
    }
}
