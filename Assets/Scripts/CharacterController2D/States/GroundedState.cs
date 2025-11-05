using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class GroundedState : ICharacterState
    {
        private StateContext StateContext;

        private Vector2 Movement;

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

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
            
            Movement = StateContext.DriverSnapshot.Movement * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
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

        public void Update()
        {
            StateContext.JumpConsumed = false;

            StateContext.Animator.SetBool("IsGrounded", true);
            StateContext.Animator.SetBool("HasXVelocity", Movement.x != 0);
            StateContext.SpriteRenderer.flipX = Movement.x < 0;
        }
    }
}
