using UnityEngine;

namespace Octobass.Waves.Character
{
    public class GroundedState : ICharacterState
    {
        private StateContext StateContext;

        public GroundedState(StateContext stateContext)
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
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (StateContext.DriverSnapshot.JumpPressed)
            {
                return CharacterStateId.Jumping;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsOnPlatform())
            {
                return CharacterStateId.Riding;
            }
            else if (!StateContext.CharacterController2DCollisionDetector.IsGrounded())
            {
                StateContext.CoyoteAllowed = true;

                return CharacterStateId.Falling;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsTouchingWall() && StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.WallClimb;
            }
            else
            {
                return null;
            }
        }
    }
}
