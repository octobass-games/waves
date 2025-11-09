using UnityEngine;

namespace Octobass.Waves.Character
{
    [CreateAssetMenu]
    public class MovementConfig : ScriptableObject
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

        [Tooltip("The skin width to apply when detecting wall jumps")]
        public float WallJumpSkinWidth;

        [Tooltip("The jump height of the character")]
        public float JumpHeight;

        [Tooltip("The coyote time for the character (milliseconds)")]
        public float CoyoteTime;

        [Tooltip("The modifier to apply to gravity when falling")]
        public float FallingGravityModifier;

        [Tooltip("The movement speed modifier to apply when moving horizontally through the air")]
        public float AirMovementSpeedModifier;

        [Tooltip("The amount of time to ignore horizontal input for after starting a wall jump")]
        public float WallJumpInputFreezeTime;

        [Tooltip("The speed at which the character will slide down walls")]
        public float WallSlideSpeed;

        [Tooltip("The speed at which the character will climb walls")]
        public float WallClimbSpeed;

        [Tooltip("The speed at which the character will swim above the surface of liquids")]
        public float SwimmingSpeed;

        [Tooltip("The height above the water that the character will sit when swimming above the surface of liquids")]
        public float SwimmingBobHeight;

        [Tooltip("The movement speed modifier to apply when moving underwater")]
        public float DivingSpeedModifier;

        [Tooltip("The contact filter to apply when detecting ground collisions")]
        public ContactFilter2D GroundContactFilter;

        [Tooltip("The contact filter to apply when detecting rideable collisions")]
        public ContactFilter2D RideableContactFilter;

        [Tooltip("The contact filter to apply when detecting water collisions")]
        public ContactFilter2D WaterContactFilter;
    }
}
