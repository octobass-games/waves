using UnityEngine;

namespace Octobass.Waves.Character
{
    public class RidingState : ICharacterState
    {
        private StateContext StateContext;
        private IRideable Rideable;

        public RidingState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            Rideable = StateContext.CharacterController2DCollisionDetector.GetPlatform();
        }

        public void Exit()
        {
            Rideable = null;
        }

        public void Tick()
        {
            StateContext.MovementIntent.Displacement = Rideable.GetDisplacement() + new Vector2(StateContext.DriverSnapshot.Movement.x, 0) * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }
    }
}
