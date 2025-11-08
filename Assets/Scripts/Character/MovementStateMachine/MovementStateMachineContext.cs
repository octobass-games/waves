using UnityEngine;

namespace Octobass.Waves.Character
{
    public class MovementStateMachineContext
    {
        public readonly Rigidbody2D Body;
        public readonly Animator Animator;
        public CharacterController2DDriverSnapshot DriverSnapshot;
        public MovementIntent MovementIntent;
        public MovementConfig CharacterControllerConfig;
        public MovementControllerCollisionDetector CharacterController2DCollisionDetector;
        public bool CoyoteAllowed;
        public float Velocity;
        public Vector2 WallJumpDirection;
        public bool IsTouchingWallForWallJump;

        public MovementStateMachineContext(Rigidbody2D body, CharacterController2DDriverSnapshot driverSnapshot, MovementIntent movementIntent, MovementConfig characterControllerConfig, MovementControllerCollisionDetector characterController2DCollisionDetector)
        {
            Body = body;
            DriverSnapshot = driverSnapshot;
            MovementIntent = movementIntent;
            CharacterControllerConfig = characterControllerConfig;
            CharacterController2DCollisionDetector = characterController2DCollisionDetector;
        }
    }
}
