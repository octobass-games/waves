using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class AttackStateMachine
    {
        private AttackStateMachineContext Context;
        private IAttackState CurrentState;
        private AttackStateId CurrentStateId;

        private Dictionary<AttackStateId, IAttackState> StateRegistry;

        public AttackStateMachine(AttackStateMachineContext context)
        {
            Context = context;

            StateRegistry = new()
            {
                { AttackStateId.Passive, new PassiveState(Context) },
                { AttackStateId.Attacking, new AttackingState(Context) },
            };

            CurrentState = StateRegistry[AttackStateId.Passive];
            CurrentStateId = AttackStateId.Passive;
        }

        public void Tick()
        {
            CurrentState.Tick();
        }

        public bool EvaluateTransitions()
        {
            AttackStateId? nextState = GetNextTransition();

            if (nextState.HasValue)
            {
                if (!StateRegistry.TryGetValue(nextState.Value, out CurrentState))
                {
                    Debug.Log($"[AttackStateMachine]: Could not find state to transition to - {nextState.Value}");
                    CurrentState = StateRegistry[AttackStateId.Passive];
                    CurrentStateId = AttackStateId.Passive;
                }
                else
                {
                    CurrentStateId = nextState.Value;
                }

                Debug.Log($"[AttackStateMachine]: Entering - {CurrentState}");
                CurrentState.Enter();

                return true;
            }

            return false;
        }

        private AttackStateId? GetNextTransition()
        {
            if (AttackStateMachineTransitionRegistry.Transitions.TryGetValue(CurrentStateId, out List<AttackStateMachineTransition> transitions))
            {
                foreach (var transition in transitions)
                {
                    if (StateRegistry.ContainsKey(transition.Target) && transition.IsSatisfied(Context))
                    {
                        return transition.Target;
                    }
                }
            }
            else
            {
                Debug.LogWarning($"[AttackStateMachine]: Could not find transitions for {CurrentStateId}");
            }

            return null;
        }
    }
}
