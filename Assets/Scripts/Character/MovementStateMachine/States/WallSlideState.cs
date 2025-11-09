using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallSlideState : CharacterState
    {
        private MovementStateMachineContext StateContext;

        public WallSlideState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public override void Tick()
        {
            StateContext.MovementIntent.Displacement = Vector2.down * StateContext.CharacterControllerConfig.WallSlideSpeed * Time.fixedDeltaTime;
        }
    }
}
