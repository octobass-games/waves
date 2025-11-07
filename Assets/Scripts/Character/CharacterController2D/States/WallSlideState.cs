using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallSlideState : ICharacterState
    {
        private StateContext StateContext;

        public WallSlideState(StateContext stateContext)
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
            StateContext.MovementIntent.Displacement = Vector2.down * StateContext.CharacterControllerConfig.WallSlideSpeed * Time.fixedDeltaTime;
        }
    }
}
