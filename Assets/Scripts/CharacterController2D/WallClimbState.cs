using UnityEngine;

namespace Octobass.Waves.CharacterController2D
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

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement.y = StateContext.DriverSnapshot.Movement.y * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (!StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.DriverSnapshot.Movement.y > 0)
            {
                return CharacterStateId.WallJump;
            }

            return null;
        }

        public void Update()
        {
        }
    }
}
