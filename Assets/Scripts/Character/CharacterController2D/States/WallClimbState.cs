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

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement.y = StateContext.DriverSnapshot.Climbing.y * StateContext.CharacterControllerConfig.WallClimbSpeed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (StateContext.DriverSnapshot.GrabReleased || !StateContext.CharacterController2DCollisionDetector.IsTouchingWall())
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.DriverSnapshot.JumpPressed)
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
