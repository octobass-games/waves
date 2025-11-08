using System;

namespace Octobass.Waves.Character
{
    public class MovementStateTransition
    {
        public CharacterStateId Target;
        public Func<MovementStateMachineContext, bool> IsSatisfied;

        public MovementStateTransition(CharacterStateId target, Func<MovementStateMachineContext, bool> isSatisfied)
        {
            Target = target;
            IsSatisfied = isSatisfied;
        }
    }
}
