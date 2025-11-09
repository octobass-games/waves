using UnityEngine;

namespace Octobass.Waves.Character
{
    public class RidingState : CharacterState
    {
        private MovementStateMachineContext StateContext;
        private IRideable Rideable;

        public RidingState(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;
        }

        public override void Enter()
        {
            Rideable = StateContext.CharacterController2DCollisionDetector.GetPlatform();
        }

        public override void Exit()
        {
            Rideable = null;
        }

        public override void Tick()
        {
            StateContext.MovementIntent.Displacement = Rideable.GetDisplacement() + new Vector2(StateContext.DriverSnapshot.Movement.x, 0) * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }
    }
}
