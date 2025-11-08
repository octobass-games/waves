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
        public CharacterController2DConfig CharacterControllerConfig;
        public AttackMove Attack;

        private CharacterController2DCollisionDetector CollisionDetector;

        private MovementStateMachine MovementStateMachine;
        private MovementStateMachineContext MovementStateMachineContext;
        private AttackStateMachine AttackStateMachine;
        private AttackStateMachineContext AttackStateMachineContext;

        private ContactFilter2D AllGroundContactFilter;

        private bool AnimatorUpdated;
        private Vector2 Displacement;

        void Awake()
        {
            CollisionDetector = new CharacterController2DCollisionDetector(Body, CharacterControllerConfig);

            MovementStateMachineContext = new MovementStateMachineContext(Body, Animator, SpriteRenderer, new CharacterController2DDriverSnapshot(), new MovementIntent(), CharacterControllerConfig, CollisionDetector);
            MovementStateMachine = new MovementStateMachine(MovementStateMachineContext);

            AttackStateMachineContext = new AttackStateMachineContext(MovementStateMachine.CurrentStateId, Attack, new CharacterController2DDriverSnapshot());
            AttackStateMachine = new AttackStateMachine(AttackStateMachineContext);

            AllGroundContactFilter = new()
            {
                useLayerMask = true,
                layerMask = CharacterControllerConfig.GroundContactFilter.layerMask | CharacterControllerConfig.RideableContactFilter.layerMask
            };
        }

        void Update()
        {
            CharacterController2DDriverSnapshot snapshot = Driver.TakeSnapshot();
            
            MovementStateMachineContext.DriverSnapshot = snapshot;
            AttackStateMachineContext.DriverSnapshot = snapshot; 
            
            CharacterStateId currentState = MovementStateMachine.CurrentStateId;

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

            Animator.SetBool("HasXVelocity", Displacement.x != 0);
            Animator.SetBool("HasYVelocity", Displacement.y != 0);
            SpriteRenderer.flipX = (currentState == CharacterStateId.WallClimb || currentState == CharacterStateId.WallSlide) ? CollisionDetector.IsTouchingLeftWall() : Displacement.x < 0;
        }

        void FixedUpdate()
        {
            AttackStateMachine.Tick();
            MovementStateMachine.Tick();

            Displacement = MovementStateMachineContext.MovementIntent.Displacement;
            Vector2 normalizedDisplacement = Displacement == Vector2.zero ? Vector2.zero : Displacement.normalized;

            Vector2 safeXDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectX(), Displacement.x, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Vector2 safeYDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectY(), Displacement.y, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Body.MovePosition(Body.position + safeXDisplacement + safeYDisplacement);

            AnimatorUpdated = !MovementStateMachine.EvaluateTransitions();
            AttackStateMachine.EvaluateTransitions();

            Driver.Consume();
        }

        public void OnAbilityItemPickedUp(AbilityItemInstance ability)
        {
            MovementStateMachine.AddState(ability.NewState);
        }
    }
}
