namespace Octobass.Waves.Movement
{
    public class DivingState : CharacterState
    {
        private readonly MovementConfig Config;

        public DivingState(MovementConfig config)
        {
            Config = config;
        }

        public override StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot)
        {
            return new StateSnapshot()
            {
                Velocity = driverSnapshot.Swimming * Config.DivingSpeedModifier * Config.Speed
            };
        }
    }
}
