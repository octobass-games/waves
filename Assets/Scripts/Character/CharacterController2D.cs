using Octobass.Waves.Attack;
using Octobass.Waves.Extensions;
using Octobass.Waves.Item;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class CharacterController2D : MonoBehaviour
    {
        public Rigidbody2D Body;
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        public CharacterController2DDriver Driver;
        public MovementControllerConfig CharacterControllerConfig;
        public AttackMove Attack;

        private MovementControllerCollisionDetector CollisionDetector;

        private MovementStateMachine MovementStateMachine;
        private MovementStateMachineContext MovementStateMachineContext;
        private AttackStateMachine AttackStateMachine;
        private AttackStateMachineContext AttackStateMachineContext;

        private ContactFilter2D AllGroundContactFilter;

        private bool AnimatorUpdated;
        private bool AttackAnimationUpdated;
        private Vector2 Displacement;

        void Update()
        {
            CharacterController2DDriverSnapshot snapshot = Driver.TakeSnapshot();
            
            MovementStateMachineContext.DriverSnapshot = snapshot;
            AttackStateMachineContext.DriverSnapshot = snapshot; 
            
            CharacterStateId currentState = MovementStateMachine.CurrentStateId;
            AttackStateId attackState = AttackStateMachine.CurrentStateId;

            if (!AnimatorUpdated)
            {
                switch (currentState)
                {
                    case CharacterStateId.Grounded:
                        Animator.SetTrigger("IsGrounded");
                        break;
                    case CharacterStateId.Jumping:
                        Animator.SetTrigger("Jump");
                        break;
                    case CharacterStateId.Falling:
                        Animator.SetTrigger("Fall");
                        break;
                    case CharacterStateId.WallClimb:
                        Animator.SetTrigger("WallClimb");
                        break;
                    default:
                        break;
                }

                AnimatorUpdated = true;
            }
            
            if (!AttackAnimationUpdated)
            {
                switch (attackState)
                {
                    case AttackStateId.Attacking:
                        Debug.Log($"Setting melee trigger - {attackState}");
                        Animator.SetTrigger("MeleeAttack");
                        break;
                    default:
                        break;
                }

                AttackAnimationUpdated = true;
            }

            Animator.SetBool("HasXVelocity", Displacement.x != 0);
            Animator.SetBool("HasYVelocity", Displacement.y != 0);
            SpriteRenderer.flipX = (currentState == CharacterStateId.WallClimb || currentState == CharacterStateId.WallSlide) ? CollisionDetector.IsTouchingLeftWall() : Displacement.x < 0;
        }
    }
}
