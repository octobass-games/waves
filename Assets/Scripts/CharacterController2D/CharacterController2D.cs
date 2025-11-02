using Octobass.Waves.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    // TODO: Fix jump consumption
    public class CharacterController2D : MonoBehaviour
    {
        public Rigidbody2D Body;
        public Animator Animator;
        public CharacterController2DDriver Driver;
        public CharacterController2DConfig CharacterControllerConfig;

        private Dictionary<CharacterStateId, ICharacterState> StateRegistry;
        private ICharacterState State;
        private StateContext StateContext;

        void Awake()
        {
            StateContext = new StateContext(Body, Animator, new CharacterController2DDriverSnapshot(), new MovementIntent(), CharacterControllerConfig);

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
        }

        void Update()
        {
            StateContext.DriverSnapshot = Driver.TakeSnapshot();

            State.Update();
        }

        void FixedUpdate()
        {
            State.FixedUpdate();

            Vector2 displacement = StateContext.MovementIntent.Displacement;
            Vector2 normalizedDisplacement = displacement == Vector2.zero ? Vector2.zero : displacement.normalized;

            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = CharacterControllerConfig.GroundContactFilter.layerMask | CharacterControllerConfig.RideableContactFilter.layerMask;

            Body.SafeMovePosition(normalizedDisplacement.ProjectX(), displacement.x, CharacterControllerConfig.SkinWidth, contactFilter);
            Body.SafeMovePosition(normalizedDisplacement.ProjectY(), displacement.y, CharacterControllerConfig.SkinWidth, contactFilter);

            CharacterStateId? nextState = State.GetTransition();

            if (nextState.HasValue)
            {
                State.Exit();

                if (!StateRegistry.TryGetValue(nextState.Value, out State))
                {
                    Debug.Log($"[CharacterController]: Could not find state to transition to - {nextState.Value}");
                    State = StateRegistry[CharacterStateId.Grounded];
                }

                Debug.Log($"[CharacterController]: Entering - {State}");
                State.Enter();
            }
        }
    }
}
