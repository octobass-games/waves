using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallClimbState : ICharacterState
    {
        private MovementStateMachineContext StateContext;

        public WallClimbState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            StateContext.MovementIntent.Displacement.x = 0;
        }

        public void Exit()
        {
        }

        public void Tick()
        {
            StateContext.MovementIntent.Displacement.y = StateContext.DriverSnapshot.Climbing.y * StateContext.CharacterControllerConfig.WallClimbSpeed * Time.fixedDeltaTime;
        }
    }
}
