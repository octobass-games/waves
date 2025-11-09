using Octobass.Waves.Item;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class CharacterBrain : MonoBehaviour
    {
        public MovementStateMachine MovementStateMachine;
        public AttackController AttackController;
        public AnimationController AnimationController;

        public CharacterController2DDriver Driver;
        private CharacterController2DDriverSnapshot Snapshot = new();

        private MovementSnapshot MovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);

        void Update()
        {
            Snapshot = Driver.TakeSnapshot();

            AnimationController.Tick(MovementSnapshot, AttackController.GetCurrentState());
        }

        void FixedUpdate()
        {
            AttackController.Tick(Snapshot);

            MovementSnapshot = MovementStateMachine.Tick(Snapshot);

            Driver.Consume();
        }

        public void OnAbilityItemPickedUp(AbilityItemInstance ability)
        {
            MovementStateMachine.AddState(ability.NewState);
        }
    }
}
