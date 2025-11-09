using UnityEngine;

namespace Octobass.Waves.Character
{
    public class DivingState : CharacterState
    {
        public MovementStateMachineContext Context;

        public DivingState(MovementStateMachineContext context)
        {
            Context = context;
        }

        public override void Tick()
        {
            Context.MovementIntent.Displacement = Context.DriverSnapshot.Swimming * Context.CharacterControllerConfig.DivingSpeedModifier * Context.CharacterControllerConfig.Speed * Time.fixedDeltaTime;
        }
    }
}
