using UnityEngine;

namespace Octobass.Waves.CharacterController2D
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

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = Rideable.GetDisplacement() + new Vector2(StateContext.DriverSnapshot.Movement.x, 0) * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            IRideable platform = StateContext.CharacterController2DCollisionDetector.GetPlatform();

            if (platform == null)
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.DriverSnapshot.JumpPressed)
            {
                return CharacterStateId.Jumping;
            }

            return null;
        }

        public void Update()
        {
        }
    }
}
