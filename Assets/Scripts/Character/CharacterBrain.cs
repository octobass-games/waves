using Octobass.Waves.Item;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class CharacterBrain : MonoBehaviour
    {
        public MovementStateMachine MovementController;
        public AttackController AttackController;

        public Rigidbody2D Body;
        public MovementConfig CharacterControllerConfig;
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

        private MovementSnapshot CurrentMovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);
        private MovementSnapshot PreviousMovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);

        void Awake()
        {
            CollisionDetector = new MovementControllerCollisionDetector(Body, CharacterControllerConfig);
        }

        void Update()
        {
            Snapshot = Driver.TakeSnapshot();

            CurrentAttackState = AttackController.GetCurrentState();

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

            PreviousMovementSnapshot = CurrentMovementSnapshot;

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

            Animator.SetBool("HasXVelocity", CurrentMovementSnapshot.Displacement.x != 0);
            Animator.SetBool("HasYVelocity", CurrentMovementSnapshot.Displacement.y != 0);
            SpriteRenderer.flipX = CurrentMovementSnapshot.FacingDirection == Vector2.left;
        }

        void FixedUpdate()
        {
            AttackController.Tick(Snapshot);

            CurrentMovementSnapshot = MovementController.Tick(Snapshot);

            Driver.Consume();
        }

        public void OnAbilityItemPickedUp(AbilityItemInstance ability)
        {
            MovementController.AddState(ability.NewState);
        }
    }
}
