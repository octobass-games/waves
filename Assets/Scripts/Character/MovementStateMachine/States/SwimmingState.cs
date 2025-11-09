using Octobass.Waves.Extensions;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class SwimmingState : CharacterState
    {
        private MovementStateMachineContext Context;

        public SwimmingState(MovementStateMachineContext context)
        {
            Context = context;
        }

        public override void Tick()
        {
            Collider2D waterCollider = Context.CharacterController2DCollisionDetector.DetectWater();

            var characterY = Context.Body.GetComponent<BoxCollider2D>().bounds.max.y;
            var colliderY = waterCollider.bounds.max.y;
            var bobPositionY = colliderY + Context.CharacterControllerConfig.SwimmingBobHeight;

            float verticalDistanceFromBobHeight = characterY - bobPositionY;

            Debug.Log($"[SwimmingState]: characterY - {characterY}, colliderY - {colliderY}, bobPositionY - {bobPositionY}, verticalDistanceFromBobHeight - {verticalDistanceFromBobHeight}");

            if (verticalDistanceFromBobHeight > 0)
            {
                Context.MovementIntent.Displacement = new Vector2(0, Mathf.Max(-1 * Time.fixedDeltaTime, -verticalDistanceFromBobHeight));
            }
            else if (verticalDistanceFromBobHeight < 0)
            {
                Context.MovementIntent.Displacement = new Vector2(0, Mathf.Min(-1 * Time.fixedDeltaTime, verticalDistanceFromBobHeight));
            }
            else
            {
                Context.MovementIntent.Displacement = Context.DriverSnapshot.Movement.ProjectX() * Context.CharacterControllerConfig.SwimmingSpeed * Time.fixedDeltaTime;
            }
        }
    }
}
