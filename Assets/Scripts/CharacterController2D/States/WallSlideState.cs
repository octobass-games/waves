using UnityEngine;

namespace Octobass.Waves.CharacterController2D
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

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = Vector2.down * StateContext.CharacterControllerConfig.WallSlideSpeed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (StateContext.CharacterController2DCollisionDetector.IsGrounded())
            {
                return CharacterStateId.Grounded;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsTouchingRightWall() && StateContext.DriverSnapshot.Movement.x <= 0 || StateContext.CharacterController2DCollisionDetector.IsTouchingLeftWall() && StateContext.DriverSnapshot.Movement.x >= 0 || !StateContext.CharacterController2DCollisionDetector.IsTouchingWall())
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.DriverSnapshot.JumpPressed)
            {
                return CharacterStateId.WallJump;
            }
            else if (StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.WallClimb;
            }

            return null;
        }

        public void Update()
        {
        }
    }
}
