using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallClimbState : CharacterState
    {
        private MovementStateMachineContext StateContext;

        public WallClimbState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public override void Enter()
        {
            StateContext.MovementIntent.Displacement.x = 0;
        }

        public override void Tick()
        {
            StateContext.MovementIntent.Displacement.y = StateContext.DriverSnapshot.Climbing.y * StateContext.CharacterControllerConfig.WallClimbSpeed * Time.fixedDeltaTime;
        }
    }
}
