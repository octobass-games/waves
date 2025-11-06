using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallClimbState : ICharacterState
    {
        private StateContext StateContext;

        private bool AnimatorUpdated;
        private float Movement;

        public WallClimbState(StateContext stateContext)
        {
            StateContext = stateContext;
        }

        public void Enter()
        {
            AnimatorUpdated = false;
            StateContext.MovementIntent.Displacement.x = 0;
        }

        public void Exit()
        {
        }

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement.y = StateContext.DriverSnapshot.Climbing.y * StateContext.CharacterControllerConfig.WallClimbSpeed * Time.fixedDeltaTime;
            Movement = StateContext.DriverSnapshot.Climbing.y* StateContext.CharacterControllerConfig.WallClimbSpeed* Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (!StateContext.DriverSnapshot.GrabPressed || !StateContext.CharacterController2DCollisionDetector.IsTouchingWall())
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
            if (!AnimatorUpdated)
            {
                StateContext.Animator.SetTrigger("WallClimb");

                AnimatorUpdated = true;
            }

            StateContext.Animator.SetBool("HasYVelocity", Movement != 0);
        }
    }
}
