using UnityEngine;

namespace Octobass.Waves.Character
{
    public class GroundedState : ICharacterState
    {
        private MovementStateMachineContext StateContext;

        public GroundedState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
            StateContext.CoyoteAllowed = true;
        }
    }
}
