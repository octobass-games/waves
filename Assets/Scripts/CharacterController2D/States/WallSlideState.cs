using Octobass.Waves.CharacterController2D;
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
            if (IsTouchingWall(Vector2.down))
            {
                return CharacterStateId.Grounded;
            }
            else if (IsTouchingWall(Vector2.right) && StateContext.DriverSnapshot.Movement.x <= 0 || IsTouchingWall(Vector2.left) && StateContext.DriverSnapshot.Movement.x >= 0 || !IsTouchingWall(Vector2.right) && !IsTouchingWall(Vector2.left))
            {
                return CharacterStateId.Falling;
            }
            else if (StateContext.DriverSnapshot.Movement.y > 0)
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

        private bool IsTouchingWall(Vector2 direction)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(direction, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
    }
}
