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
            StateContext.MovementIntent.Displacement.y = StateContext.DriverSnapshot.Climbing.y * StateContext.CharacterControllerConfig.WallClimbSpeed * Time.fixedDeltaTime;
        }

        public CharacterStateId? GetTransition()
        {
            if (!StateContext.DriverSnapshot.GrabPressed || !IsTouchingWall())
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

        private bool IsTouchingWall()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int rightCount = StateContext.Body.Cast(Vector2.right, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            if (rightCount > 0)
            {
                return true;
            }

            int leftCount = StateContext.Body.Cast(Vector2.left, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            if (leftCount > 0)
            {
                return true;
            }

            return false;
        }
    }
}
