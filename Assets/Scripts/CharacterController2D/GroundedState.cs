using UnityEngine;

namespace Octobass.Waves.CharacterController2D
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

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (StateContext.DriverSnapshot.Movement.y > 0)
            {
                return CharacterStateId.Jumping;
            }
            else if (!IsGrounded())
            {
                return CharacterStateId.Falling;
            }
            else if (IsTouchingWall() && StateContext.DriverSnapshot.GrabPressed)
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
        }

        private bool IsGrounded()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(Vector2.down, StateContext.CharacterControllerConfig.CollisionContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }

        private bool IsTouchingWall()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int rightCount = StateContext.Body.Cast(Vector2.right, StateContext.CharacterControllerConfig.CollisionContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            if (rightCount > 0)
            {
                return true;
            }

            int leftCount = StateContext.Body.Cast(Vector2.left, StateContext.CharacterControllerConfig.CollisionContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            if (leftCount > 0)
            {
                return true;
            }

            return false;
        }
    }
}
