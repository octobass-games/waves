using Octobass.Waves.Attack;
using UnityEngine;

namespace Octobass.Waves.Movement
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

            Animator.SetBool("IsGrounded", CurrentMovementSnapshot.State == CharacterStateId.Grounded);
            Animator.SetBool("IsJumping", CurrentMovementSnapshot.State == CharacterStateId.Jumping || CurrentMovementSnapshot.State == CharacterStateId.WallJump);
            Animator.SetBool("IsFalling", CurrentMovementSnapshot.State == CharacterStateId.Falling);
            Animator.SetBool("IsWallClimbing", CurrentMovementSnapshot.State == CharacterStateId.WallClimb && CurrentMovementSnapshot.Displacement.y != 0);
            Animator.SetBool("IsWallHolding", CurrentMovementSnapshot.State == CharacterStateId.WallClimb && CurrentMovementSnapshot.Displacement.y == 0);
            Animator.SetBool("IsSwimming", CurrentMovementSnapshot.State == CharacterStateId.Swimming);
            Animator.SetBool("IsDiving", CurrentMovementSnapshot.State == CharacterStateId.Diving);
            Animator.SetBool("IsWallSlide", CurrentMovementSnapshot.State == CharacterStateId.WallSlide);
            Animator.SetBool("IsDashing", CurrentMovementSnapshot.State == CharacterStateId.Dashing);
            Animator.SetBool("IsLedgeClimbing", CurrentMovementSnapshot.State == CharacterStateId.LedgeClimb);

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

        public void PlayDeathAnimation()
        {
            Animator.SetTrigger("IsDying");
        }
    }
}
