namespace Octobass.Waves.Movement
{
    public class WallClimbState : CharacterState
    {
        private readonly float WallClimbSpeed;

        public WallClimbState(MovementConfig config)
        {
            WallClimbSpeed = config.WallClimbSpeed;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Climbing * WallClimbSpeed
            };
        }
    }
}
