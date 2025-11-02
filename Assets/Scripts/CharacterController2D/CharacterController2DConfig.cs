using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    [CreateAssetMenu]
    public class CharacterController2DConfig : ScriptableObject
    {
        [Tooltip("The default horizontal movement speed of the character")]
        public float Speed;

        [Tooltip("The maximum horizontal movement speed of the character")]
        public float MaxSpeed;

        [Tooltip("The maximum vertical fall speed of the character")]
        public float MaxFallSpeed;

        [Tooltip("The gravity to apply to the character")]
        public float Gravity;

        [Tooltip("The skin width of the character i.e. the movement buffer")]
        public float SkinWidth;

        [Tooltip("The jump height of the character")]
        public float JumpHeight;

        [Tooltip("The coyote time for the character (milliseconds)")]
        public float CoyoteTime;

        [Tooltip("The modifier to apply to gravity when falling")]
        public float FallingGravityModifier;

        [Tooltip("The gravity modifier to apply when the jump action is cancelled")]
        public float VariableJumpHeightGravityModifier;

        [Tooltip("The movement speed modifier to apply when moving horizontally through the air")]
        public float AirMovementSpeedModifier;

        [Tooltip("The amount of time to ignore horizontal input for after starting a wall jump")]
        public float WallJumpInputFreezeTime;

        [Tooltip("The contact filter to apply when detecting ground collisions")]
        public ContactFilter2D GroundContactFilter;

        [Tooltip("The contact filter to apply when detecting rideable collisions")]
        public ContactFilter2D RideableContactFilter;
    }
}
