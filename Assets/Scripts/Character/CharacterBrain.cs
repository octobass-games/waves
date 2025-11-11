using Octobass.Waves.Attack;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class CharacterBrain : MonoBehaviour
    {
        public MovementController MovementStateMachine;
        public AttackController AttackController;
        public AnimationController AnimationController;

        public CharacterController2DDriver Driver;

        private CharacterController2DDriverSnapshot DriverSnapshot = new();
        private MovementSnapshot MovementSnapshot = new(CharacterStateId.Grounded, Vector2.zero, Vector2.right);
        private AttackSnapshot AttackSnapshot = new(false);

        void Update()
        {
            DriverSnapshot = Driver.TakeSnapshot();
            AnimationController.Tick(MovementSnapshot, AttackSnapshot);
        }

        void FixedUpdate()
        {
            MovementSnapshot = MovementStateMachine.Tick(DriverSnapshot);
            AttackSnapshot = AttackController.Tick(DriverSnapshot, MovementSnapshot);
            Driver.Consume();
        }
    }
}
