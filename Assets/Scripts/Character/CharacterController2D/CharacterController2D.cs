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

        private CharacterController2DCollisionDetector CollisionDetector;

        private StateMachine StateMachine;
        private StateContext StateContext;

        private ContactFilter2D AllGroundContactFilter;

        private bool AnimatorUpdated;
        private Vector2 Displacement;

        void Awake()
        {
            CollisionDetector = new CharacterController2DCollisionDetector(Body, CharacterControllerConfig);

            StateContext = new StateContext(Body, Animator, SpriteRenderer, new CharacterController2DDriverSnapshot(), new MovementIntent(), CharacterControllerConfig, CollisionDetector);
            StateMachine = new StateMachine(StateContext);

            AllGroundContactFilter = new()
            {
                useLayerMask = true,
                layerMask = CharacterControllerConfig.GroundContactFilter.layerMask | CharacterControllerConfig.RideableContactFilter.layerMask
            };
        }

        void Update()
        {
            StateContext.DriverSnapshot = Driver.TakeSnapshot();
            
            CharacterStateId currentState = StateMachine.CurrentStateId;

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
            StateMachine.Tick();

            Displacement = StateContext.MovementIntent.Displacement;
            Vector2 normalizedDisplacement = Displacement == Vector2.zero ? Vector2.zero : Displacement.normalized;

            Vector2 safeXDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectX(), Displacement.x, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Vector2 safeYDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectY(), Displacement.y, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Body.MovePosition(Body.position + safeXDisplacement + safeYDisplacement);

            AnimatorUpdated = !StateMachine.EvaluateTransitions();

            Driver.Consume();
        }

        public void OnAbilityItemPickedUp(AbilityItemInstance ability)
        {
            StateMachine.AddState(ability.NewState);
        }
    }
}
