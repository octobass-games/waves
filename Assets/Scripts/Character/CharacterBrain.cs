using Octobass.Waves.Item;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class CharacterBrain : MonoBehaviour
    {
        public MovementController MovementController;
        public AttackController AttackController;

        public Rigidbody2D Body;
        public MovementControllerConfig CharacterControllerConfig;
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;

        public CharacterController2DDriver Driver;
        private CharacterController2DDriverSnapshot Snapshot = new();

        private bool AnimatorUpdated;
        private bool AttackAnimationUpdated;
        private Vector2 Displacement;

        private MovementControllerCollisionDetector CollisionDetector;

        private AttackStateId PreviousAttackState;
        private AttackStateId CurrentAttackState;

        private CharacterStateId PreviousMovementState;
        private CharacterStateId CurrentMovementState;

        void Awake()
        {
            CollisionDetector = new MovementControllerCollisionDetector(Body, CharacterControllerConfig);
        }

        void Update()
        {
            Snapshot = Driver.TakeSnapshot();

            CurrentMovementState = MovementController.GetCurrentState();
            CurrentAttackState = AttackController.GetCurrentState();

            if (PreviousMovementState != CurrentMovementState)
            {
                switch (CurrentMovementState)
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

                PreviousMovementState = CurrentMovementState;
            }

            if (PreviousAttackState != CurrentAttackState)
            {
                switch (CurrentAttackState)
                {
                    case AttackStateId.Attacking:
                        Debug.Log($"Setting melee trigger - {CurrentAttackState}");
                        Animator.SetTrigger("MeleeAttack");
                        break;
                    default:
                        break;
                }

                PreviousAttackState = CurrentAttackState;
            }

            Animator.SetBool("HasXVelocity", Displacement.x != 0);
            Animator.SetBool("HasYVelocity", Displacement.y != 0);
            SpriteRenderer.flipX = (CurrentMovementState == CharacterStateId.WallClimb || CurrentMovementState == CharacterStateId.WallSlide) ? CollisionDetector.IsTouchingLeftWall() : Displacement.x < 0;
        }

        void FixedUpdate()
        {
            AttackController.Tick(Snapshot);

            Displacement = MovementController.Tick(Snapshot);

            Driver.Consume();
        }

        public void OnAbilityItemPickedUp(AbilityItemInstance ability)
        {
            MovementController.AddState(ability.NewState);
        }
    }
}
