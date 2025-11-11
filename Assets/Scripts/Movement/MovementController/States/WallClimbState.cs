namespace Octobass.Waves.Movement
{
    public class WallClimbState : CharacterState
    {
        private readonly MovementConfig Config;

        public WallClimbState(MovementConfig config)
        {
            Config = config;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Climbing * Config.WallClimbSpeed
            };
        }
    }
}
