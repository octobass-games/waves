using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public class StateContext
    {
        public readonly Rigidbody2D Body;
        public readonly Animator Animator;
        public readonly SpriteRenderer SpriteRenderer;
        public CharacterController2DDriverSnapshot DriverSnapshot;
        public MovementIntent MovementIntent;
        public CharacterController2DConfig CharacterControllerConfig;
        public CharacterController2DCollisionDetector CharacterController2DCollisionDetector;
        public bool JumpConsumed;

        public StateContext(Rigidbody2D body, Animator animator, SpriteRenderer spriteRenderer, CharacterController2DDriverSnapshot driverSnapshot, MovementIntent movementIntent, CharacterController2DConfig characterControllerConfig, CharacterController2DCollisionDetector characterController2DCollisionDetector)
        {
            Body = body;
            Animator = animator;
            SpriteRenderer = spriteRenderer;
            DriverSnapshot = driverSnapshot;
            MovementIntent = movementIntent;
            CharacterControllerConfig = characterControllerConfig;
            CharacterController2DCollisionDetector = characterController2DCollisionDetector;
        }
    }
}
