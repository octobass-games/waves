using System;

namespace Octobass.Waves.Movement
{
    public class MovementStateTransition
    {
        public CharacterStateId Target;
        public Func<StateSnapshot, MovementDriverSnapshot, MovementControllerCollisionDetector, bool> IsSatisfied;

        public MovementStateTransition(CharacterStateId target, Func<StateSnapshot, MovementDriverSnapshot, MovementControllerCollisionDetector, bool> isSatisfied)
        {
            Target = target;
            IsSatisfied = isSatisfied;
        }
    }
}
