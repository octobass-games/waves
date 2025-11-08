using Octobass.Waves.Attack;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class AttackController : MonoBehaviour
    {
        public AttackMove Attack;

        private AttackStateMachine AttackStateMachine;
        private AttackStateMachineContext AttackStateMachineContext;

        void Awake()
        {
            AttackStateMachineContext = new AttackStateMachineContext(Attack, new CharacterController2DDriverSnapshot());
            AttackStateMachine = new AttackStateMachine(AttackStateMachineContext);
        }

        void OnActiveFrame()
        {
            Debug.Log("Active frame");
        }

        void OnRecoveryFrame()
        {
            Debug.Log("Recovery frame");
        }

        public void Tick(CharacterController2DDriverSnapshot snapshot)
        {
            AttackStateMachineContext.DriverSnapshot = snapshot;

            AttackStateMachine.Tick();
            AttackStateMachine.EvaluateTransitions();
        }

        public AttackStateId GetCurrentState()
        {
            return AttackStateMachine.CurrentStateId;
        }
    }
}
