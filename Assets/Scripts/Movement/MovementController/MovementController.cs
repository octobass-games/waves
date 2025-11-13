using Octobass.Waves.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class MovementController : MonoBehaviour
    {
        private Dictionary<CharacterStateId, CharacterState> StateRegistry = new();
        private CharacterState CurrentState;

        public Rigidbody2D Body;
        public MovementConfig CharacterControllerConfig;
        private MovementControllerCollisionDetector CollisionDetector;

        private ContactFilter2D AllGroundContactFilter;

        private CharacterStateId PreviousStateId;
        private CharacterStateId CurrentStateId;
        private StateSnapshot StateSnapshot = new();
        private Vector2 CurrentFacingDirection = Vector2.right;

        void Awake()
        {
            CollisionDetector = new MovementControllerCollisionDetector(Body, CharacterControllerConfig);

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

        public void ResetAtPosition(Vector2 position)
        {
            Body.position = position;

            CurrentStateId = CharacterStateId.Grounded;
            CurrentState = StateRegistry[CharacterStateId.Grounded];
        }

        public MovementSnapshot Tick(CharacterController2DDriverSnapshot driverSnapshot)
        {
            StateSnapshot = CurrentState.Tick(StateSnapshot, driverSnapshot);

            Vector2 displacement = StateSnapshot.Velocity * Time.fixedDeltaTime;
            Vector2 normalizedDisplacement = displacement == Vector2.zero ? Vector2.zero : displacement.normalized;

            Vector2 safeXDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectX(), displacement.x, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Vector2 safeYDisplacement = Body.GetSafeDisplacement(normalizedDisplacement.ProjectY(), displacement.y, CharacterControllerConfig.SkinWidth, AllGroundContactFilter);
            Body.MovePosition(Body.position + safeXDisplacement + safeYDisplacement);
            
            CurrentFacingDirection = GetFacingDirection(displacement, driverSnapshot, CurrentFacingDirection);

            CharacterStateId? nextState = GetNextTransition(driverSnapshot);

            if (nextState.HasValue)
            {
                CurrentState.Exit();
                PreviousStateId = CurrentStateId;

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
                CurrentState.Enter(PreviousStateId);
            }

            return new MovementSnapshot(CurrentStateId, displacement, CurrentFacingDirection);
        }

        public void AddState(CharacterStateId stateId)
        {
            if (!StateRegistry.ContainsKey(stateId))
            {
                switch (stateId)
                {
                    case CharacterStateId.Grounded:
                        StateRegistry[CharacterStateId.Grounded] = new GroundedState(CharacterControllerConfig);
                        break;
                    case CharacterStateId.Falling:
                        StateRegistry[CharacterStateId.Falling] = new FallingState(CharacterControllerConfig);
                        break;
                    case CharacterStateId.Riding:
                        StateRegistry[CharacterStateId.Riding] = new RidingState(CharacterControllerConfig, CollisionDetector);
                        break;
                    case CharacterStateId.Jumping:
                        StateRegistry[CharacterStateId.Jumping] = new JumpingState(CharacterControllerConfig);
                        break;
                    case CharacterStateId.WallClimb:
                        StateRegistry[CharacterStateId.WallClimb] = new WallClimbState(CharacterControllerConfig);
                        StateRegistry[CharacterStateId.WallSlide] = new WallSlideState(CharacterControllerConfig);
                        break;
                    case CharacterStateId.WallJump:
                        StateRegistry[CharacterStateId.WallJump] = new WallJumpState(CharacterControllerConfig, CollisionDetector);
                        break;
                    case CharacterStateId.Swimming:
                        StateRegistry[CharacterStateId.Swimming] = new SwimmingState(CharacterControllerConfig, CollisionDetector);
                        break;
                    case CharacterStateId.Diving:
                        StateRegistry[CharacterStateId.Diving] = new DivingState(CharacterControllerConfig);
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

        private CharacterStateId? GetNextTransition(CharacterController2DDriverSnapshot driverSnapshot)
        {
            if (MovementStateTransitionRegistry.Transitions.TryGetValue(CurrentStateId, out List<MovementStateTransition> transitions))
            {
                foreach (var transition in transitions)
                {
                    if (StateRegistry.ContainsKey(transition.Target) && transition.IsSatisfied(StateSnapshot, driverSnapshot, CollisionDetector))
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

        private Vector2 GetFacingDirection(Vector2 displacement, CharacterController2DDriverSnapshot driverSnapshot, Vector2 facingDirection)
        {
            if (CurrentStateId == CharacterStateId.WallClimb || CurrentStateId == CharacterStateId.WallSlide)
            {
                return CollisionDetector.IsCloseToLeftWall() ? Vector2.left : Vector2.right;
            }

            if (displacement.x > 0)
            {
                return Vector2.right;
            }
            else if (displacement.x < 0)
            {
                return Vector2.left;
            }
            else
            {
                return facingDirection;
            }
        }
    }
}
