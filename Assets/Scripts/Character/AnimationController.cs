using UnityEngine;

namespace Octobass.Waves.Character
{
    public class AnimationController : MonoBehaviour
    {
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;

        private AttackStateId PreviousAttackState;
        private AttackStateId CurrentAttackState;
        private MovementSnapshot CurrentMovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);
        private MovementSnapshot PreviousMovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);

        public void Tick(MovementSnapshot movementSnapshot, AttackStateId attackState)
        {
            PreviousMovementSnapshot = CurrentMovementSnapshot;
            CurrentMovementSnapshot = movementSnapshot;

            PreviousAttackState = CurrentAttackState;
            CurrentAttackState = attackState;

            if (PreviousMovementSnapshot.State != CurrentMovementSnapshot.State)
            {
                switch (CurrentMovementSnapshot.State)
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
            }

            if (PreviousAttackState != CurrentAttackState)
            {
                switch (CurrentAttackState)
                {
                    case AttackStateId.Attacking:
                        Animator.SetTrigger("MeleeAttack");
                        break;
                    default:
                        break;
                }

                PreviousAttackState = CurrentAttackState;
            }

            Animator.SetBool("HasXVelocity", CurrentMovementSnapshot.Displacement.x != 0);
            Animator.SetBool("HasYVelocity", CurrentMovementSnapshot.Displacement.y != 0);
            SpriteRenderer.flipX = CurrentMovementSnapshot.FacingDirection == Vector2.left;
        }
    }
}
