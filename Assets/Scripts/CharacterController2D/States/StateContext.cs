using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class StateContext
    {
        public readonly Rigidbody2D Body;
        public readonly Animator Animator;
        public CharacterController2DDriverSnapshot DriverSnapshot;
        public MovementIntent MovementIntent;
        public CharacterController2DConfig CharacterControllerConfig;
        public bool JumpConsumed;

        public StateContext(Rigidbody2D body, Animator animator, CharacterController2DDriverSnapshot driverSnapshot, MovementIntent movementIntent, CharacterController2DConfig characterControllerConfig)
        {
            Body = body;
            Animator = animator;
            DriverSnapshot = driverSnapshot;
            MovementIntent = movementIntent;
            CharacterControllerConfig = characterControllerConfig;
        }
    }
}
