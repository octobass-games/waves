using Octobass.Waves.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Character
{
    // TODO: Fix jump consumption
    public class CharacterController2D : MonoBehaviour
    {
        public Rigidbody2D Body;
        public Animator Animator;
        public SpriteRenderer SpriteRenderer;
        public CharacterController2DDriver Driver;
        public CharacterController2DConfig CharacterControllerConfig;

        private CharacterController2DCollisionDetector CollisionDetector;

        private Dictionary<CharacterStateId, ICharacterState> StateRegistry;
        private ICharacterState State;
        private StateContext StateContext;
        private ContactFilter2D AllGroundContactFilter;

        private bool AnimatorUpdated;
        private Vector2 Displacement;

        void Awake()
        {
            CollisionDetector = new CharacterController2DCollisionDetector(Body, CharacterControllerConfig);

            StateContext = new StateContext(Body, Animator, SpriteRenderer, new CharacterController2DDriverSnapshot(), new MovementIntent(), CharacterControllerConfig, CollisionDetector);

            StateRegistry = new()
            {
                { CharacterStateId.Grounded, new GroundedState(StateContext) },
                { CharacterStateId.Jumping, new JumpingState(StateContext) },
                { CharacterStateId.Falling, new FallingState(StateContext) },
                { CharacterStateId.WallClimb, new WallClimbState(StateContext) },
                { CharacterStateId.WallJump, new WallJumpState(StateContext) },
                { CharacterStateId.Riding, new RidingState(StateContext) },
                { CharacterStateId.WallSlide, new WallSlideState(StateContext) }
            };

            State = StateRegistry[CharacterStateId.Grounded];

            AllGroundContactFilter = new()
            {
                useLayerMask = true,
                layerMask = CharacterControllerConfig.GroundContactFilter.layerMask | CharacterControllerConfig.RideableContactFilter.layerMask
            };
        }

        void Update()
        {
            StateContext.DriverSnapshot = Driver.TakeSnapshot();

            if (!AnimatorUpdated)
            {
                if (State is GroundedState)
                {
                    Animator.SetTrigger("IsGrounded");
                }
                else if (State is JumpingState)
                {
                    Animator.SetTrigger("Jump");
                }
                else if (State is FallingState)
                {
                    Animator.SetTrigger("Fall");
                }
                else if (State is WallClimbState)
                {
                    Animator.SetTrigger("WallClimb");
                }
                else if (State is WallSlideState)
                {
                }
                else if (State is RidingState)
                {
                }
                else if (State is WallJumpState)
                {
                }

                AnimatorUpdated = true;
            }

            Animator.SetBool("HasXVelocity", Displacement.x != 0);
            Animator.SetBool("HasYVelocity", Displacement.y != 0);
            SpriteRenderer.flipX = (State is WallClimbState || State is WallSlideState) ? CollisionDetector.IsTouchingLeftWall() : Displacement.x < 0;
        }

        void FixedUpdate()
        {
            State.Tick();

            Displacement = StateContext.MovementIntent.Displacement;
            Vector2 normalizedDisplacement = Displacement == Vector2.zero ? Vector2.zero : Displacement.normalized;

            Vector2 safeXDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectX(), Displacement.x, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Vector2 safeYDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectY(), Displacement.y, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Body.MovePosition(Body.position + safeXDisplacement + safeYDisplacement);

            CharacterStateId? nextState = State.GetTransition();

            if (nextState.HasValue)
            {
                State.Exit();
                AnimatorUpdated = false;

                if (!StateRegistry.TryGetValue(nextState.Value, out State))
                {
                    Debug.Log($"[CharacterController]: Could not find state to transition to - {nextState.Value}");
                    State = StateRegistry[CharacterStateId.Grounded];
                }

                Debug.Log($"[CharacterController]: Entering - {State}");
                State.Enter();
            }

            Driver.Consume();
        }
    }
}
