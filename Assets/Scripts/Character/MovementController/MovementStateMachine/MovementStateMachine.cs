using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class MovementStateMachine
    {
        public CharacterStateId CurrentStateId {  get; private set; }

        private Dictionary<CharacterStateId, ICharacterState> StateRegistry;
        private ICharacterState CurrentState;
        private MovementStateMachineContext StateContext;

        public MovementStateMachine(MovementStateMachineContext stateContext)
        {
            StateContext = stateContext;

            StateRegistry = new()
            {
                { CharacterStateId.Grounded, new GroundedState(StateContext) },
                { CharacterStateId.Falling, new FallingState(StateContext) },
                { CharacterStateId.Riding, new RidingState(StateContext) },
            };

            CurrentState = StateRegistry[CharacterStateId.Grounded];
            CurrentStateId = CharacterStateId.Grounded;
        }

        public void Tick()
        {
            CurrentState.Tick();
        }

        public bool EvaluateTransitions()
        {
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

                return true;
            }

            return false;
        }

        public void AddState(CharacterStateId stateId)
        {
            if (!StateRegistry.ContainsKey(stateId))
            {
                switch (stateId)
                {
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
    }
}
