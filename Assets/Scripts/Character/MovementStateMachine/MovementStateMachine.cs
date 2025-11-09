using Octobass.Waves.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class MovementStateMachine : MonoBehaviour
    {
        private Dictionary<CharacterStateId, CharacterState> StateRegistry = new();
        private CharacterState CurrentState;
        private CharacterStateId CurrentStateId;
        private MovementStateMachineContext StateContext;

        public Rigidbody2D Body;
        public MovementConfig CharacterControllerConfig;
        private MovementControllerCollisionDetector CollisionDetector;

        private ContactFilter2D AllGroundContactFilter;

        void Awake()
        {

            CollisionDetector = new MovementControllerCollisionDetector(Body, CharacterControllerConfig);

            StateContext = new MovementStateMachineContext(Body, new CharacterController2DDriverSnapshot(), new MovementIntent(), CharacterControllerConfig, CollisionDetector);

            AddState(CharacterStateId.Grounded);
            AddState(CharacterStateId.Falling);
            AddState(CharacterStateId.Riding);

            CurrentState = StateRegistry[CharacterStateId.Grounded];
            CurrentStateId = CharacterStateId.Grounded;
            
            AllGroundContactFilter = new()
            {
                useLayerMask = true,
                layerMask = CharacterControllerConfig.GroundContactFilter.layerMask | CharacterControllerConfig.RideableContactFilter.layerMask
            };
        }

        public MovementSnapshot Tick(CharacterController2DDriverSnapshot snapshot)
        {
            StateContext.DriverSnapshot = snapshot;

            CurrentState.Tick();

            Vector2 displacement = StateContext.MovementIntent.Displacement;
            Vector2 normalizedDisplacement = displacement == Vector2.zero ? Vector2.zero : displacement.normalized;

            Vector2 safeXDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectX(), displacement.x, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Vector2 safeYDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectY(), displacement.y, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Body.MovePosition(Body.position + safeXDisplacement + safeYDisplacement);
            
            CharacterStateId? nextState = GetNextTransition();

            if (nextState.HasValue)
            {
                CurrentState.Exit();

                if (!StateRegistry.TryGetValue(nextState.Value, out CurrentState))
                {
                    Debug.Log($"[MovementStateMachine]: Could not find state to transition to - {nextState.Value}");
                    CurrentState = StateRegistry[CharacterStateId.Grounded];
                    CurrentStateId = CharacterStateId.Grounded;
                }
                else
                {
                    CurrentStateId = nextState.Value;
                }

                Debug.Log($"[MovementStateMachine]: Entering - {CurrentState}");
                CurrentState.Enter();
            }

            return new MovementSnapshot(CurrentStateId, displacement, GetFacingDirection(displacement));
        }

        public void AddState(CharacterStateId stateId)
        {
            if (!StateRegistry.ContainsKey(stateId))
            {
                switch (stateId)
                {
                    case CharacterStateId.Grounded:
                        StateRegistry[CharacterStateId.Grounded] = new GroundedState(StateContext);
                        break;
                    case CharacterStateId.Falling:
                        StateRegistry[CharacterStateId.Falling] = new FallingState(StateContext);
                        break;
                    case CharacterStateId.Riding:
                        StateRegistry[CharacterStateId.Riding] = new RidingState(StateContext);
                        break;
                    case CharacterStateId.Jumping:
                        StateRegistry[CharacterStateId.Jumping] = new JumpingState(StateContext);
                        break;
                    case CharacterStateId.WallClimb:
                        StateRegistry[CharacterStateId.WallClimb] = new WallClimbState(StateContext);
                        StateRegistry[CharacterStateId.WallSlide] = new WallSlideState(StateContext);
                        break;
                    case CharacterStateId.WallJump:
                        StateRegistry[CharacterStateId.WallJump] = new WallJumpState(StateContext);
                        break;
                    case CharacterStateId.Swimming:
                        StateRegistry[CharacterStateId.Swimming] = new SwimmingState(StateContext);
                        break;
                    default:
                        Debug.LogWarning($"[MovementStateMachine]: Trying to add a state that is not supported - {stateId}");
                        break;
                }
            }
            else
            {
                Debug.LogWarning($"[MovementStateMachine]: State already exists in state machine - {stateId}");
            }
        }

        private CharacterStateId? GetNextTransition()
        {
            if (MovementStateTransitionRegistry.Transitions.TryGetValue(CurrentStateId, out List<MovementStateTransition> transitions))
            {
                foreach (var transition in transitions)
                {
                    if (StateRegistry.ContainsKey(transition.Target) && transition.IsSatisfied(StateContext))
                    {
                        return transition.Target;
                    }
                }
            }
            else
            {
                Debug.LogWarning($"[MovementStateMachine]: Could not find transitions for {CurrentStateId}");
            }

            return null;
        }

        private Vector2 GetFacingDirection(Vector2 displacement)
        {
            if (CurrentStateId == CharacterStateId.WallClimb || CurrentStateId == CharacterStateId.WallSlide)
            {
                return CollisionDetector.IsCloseToLeftWall() ? Vector2.left : Vector2.right;
            }

            return displacement.x < 0 ? Vector2.left : Vector2.right;
        }
    }
}
