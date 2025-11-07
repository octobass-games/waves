using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallClimbState : ICharacterState
    {
        private StateContext StateContext;

        public WallClimbState(StateContext stateContext)
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
