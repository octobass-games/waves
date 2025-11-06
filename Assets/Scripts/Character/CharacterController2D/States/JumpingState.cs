using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class JumpingState : ICharacterState
    {
        private StateContext StateContext;

        private float Velocity;
        private Vector2 Movement;

        public JumpingState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            Velocity = Mathf.Sqrt(2 * StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.JumpHeight); ;
        }

        public void Exit()
        {
            Velocity = 0;
        }

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime + Vector2.up * Velocity * Time.fixedDeltaTime;
            Movement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime + Vector2.up * Velocity * Time.fixedDeltaTime; ;

            Velocity -= StateContext.CharacterControllerConfig.Gravity * Time.fixedDeltaTime;

            if (StateContext.DriverSnapshot.JumpReleased)
            {
                Velocity = 0;
            }
        }

        public CharacterStateId? GetTransition()
        {
            if (Velocity <= 0 || StateContext.CharacterController2DCollisionDetector.IsTouchingCeiling())
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsTouchingWall() && StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.WallClimb;
            }
            else if (StateContext.CharacterController2DCollisionDetector.IsCloseToWall() && StateContext.DriverSnapshot.JumpPressed)
            {
                return CharacterStateId.WallJump;
            }
            else
            {
                return null;
            }
        }

        public void Update()
        {
            StateContext.JumpConsumed = true;
            StateContext.Animator.SetBool("IsGrounded", false);
            StateContext.SpriteRenderer.flipX = Movement.x < 0;
        }
    }
}
