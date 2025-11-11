using System;

namespace Octobass.Waves.Movement
{
    public class MovementStateTransition
    {
        public CharacterStateId Target;
        public Func<StateSnapshot, CharacterController2DDriverSnapshot, MovementControllerCollisionDetector, bool> IsSatisfied;

        public MovementStateTransition(CharacterStateId target, Func<StateSnapshot, CharacterController2DDriverSnapshot, MovementControllerCollisionDetector, bool> isSatisfied)
        {
            Target = target;
            IsSatisfied = isSatisfied;
        }
    }
}
