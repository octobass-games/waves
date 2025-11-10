using UnityEngine;

namespace Octobass.Waves.Character
{
    public class AnimationController : MonoBehaviour
    {
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;

        private AttackSnapshot PreviousAttackState = new(false);
        private AttackSnapshot CurrentAttackState = new(false);
        private MovementSnapshot CurrentMovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);
        private MovementSnapshot PreviousMovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);

        public void Tick(MovementSnapshot movementSnapshot, AttackSnapshot attackSnapshot)
        {
            PreviousMovementSnapshot = CurrentMovementSnapshot;
            CurrentMovementSnapshot = movementSnapshot;

            PreviousAttackState = CurrentAttackState;
            CurrentAttackState = attackSnapshot;

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

            if (PreviousAttackState.IsAttacking != CurrentAttackState.IsAttacking && CurrentAttackState.IsAttacking)
            {
                Animator.SetTrigger("MeleeAttack");
            }

            Animator.SetBool("HasXVelocity", CurrentMovementSnapshot.Displacement.x != 0);
            Animator.SetBool("HasYVelocity", CurrentMovementSnapshot.Displacement.y != 0);
            SpriteRenderer.flipX = CurrentMovementSnapshot.FacingDirection == Vector2.left;
        }

        public void PlayUpgradeAnimation()
        {
            Animator.SetTrigger("StaffUpgrade");
        }
    }
}
