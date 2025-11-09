using UnityEngine;

namespace Octobass.Waves.Character
{
    public class WallSlideState : CharacterState
    {
        private readonly float WallSlideSpeed;

        public WallSlideState(MovementConfig config)
        {
            WallSlideSpeed = config.WallSlideSpeed;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = Vector2.down * WallSlideSpeed
            };
        }
    }
}
