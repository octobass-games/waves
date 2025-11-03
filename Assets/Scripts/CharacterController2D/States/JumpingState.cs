using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class JumpingState : ICharacterState
    {
        private StateContext StateContext;

        private float InitialVelocity;
        private float Velocity;

        public JumpingState(StateContext stateContext)
        {
            StateContext = stateContext;

            InitialVelocity = Mathf.Sqrt(2 * StateContext.CharacterControllerConfig.Gravity * StateContext.CharacterControllerConfig.JumpHeight);
        }

        public void Enter()
        {
            Velocity = InitialVelocity;
        }

        public void Exit()
        {
            Velocity = 0;
        }

        public void FixedUpdate()
        {
            StateContext.MovementIntent.Displacement = StateContext.DriverSnapshot.Movement.ProjectX() * StateContext.CharacterControllerConfig.AirMovementSpeedModifier * StateContext.CharacterControllerConfig.Speed * Time.fixedDeltaTime + Vector2.up * Velocity * Time.fixedDeltaTime;

            Velocity -= StateContext.CharacterControllerConfig.Gravity * Time.fixedDeltaTime;

            if (StateContext.DriverSnapshot.JumpReleased)
            {
                Velocity = 0;
            }
        }

        public CharacterStateId? GetTransition()
        {
            if (Velocity <= 0 || IsTouchingRoof())
            {
                return CharacterStateId.Falling;
            }
            else if (IsTouchingWall() && StateContext.DriverSnapshot.GrabPressed)
            {
                return CharacterStateId.WallClimb;
            }
            else if (IsTouchingWall(StateContext.CharacterControllerConfig.WallJumpSkinWidth) && StateContext.DriverSnapshot.JumpPressed)
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
        }

        private bool IsTouchingWall(float distance = 0f)
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int rightCount = StateContext.Body.Cast(Vector2.right, StateContext.CharacterControllerConfig.GroundContactFilter, hits, distance + StateContext.CharacterControllerConfig.SkinWidth);

            if (rightCount > 0)
            {
                return true;
            }

            int leftCount = StateContext.Body.Cast(Vector2.left, StateContext.CharacterControllerConfig.GroundContactFilter, hits, distance + StateContext.CharacterControllerConfig.SkinWidth);

            if (leftCount > 0)
            {
                return true;
            }

            return false;
        }

        private bool IsTouchingRoof()
        {
            RaycastHit2D[] hits = new RaycastHit2D[1];

            int count = StateContext.Body.Cast(Vector2.up, StateContext.CharacterControllerConfig.GroundContactFilter, hits, StateContext.CharacterControllerConfig.SkinWidth);

            return count > 0;
        }
    }
}
