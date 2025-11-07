using System;

namespace Octobass.Waves.Character
{
    public class Transition
    {
        public CharacterStateId Target;
        public Func<StateContext, bool> IsSatisfied;

        public Transition(CharacterStateId target, Func<StateContext, bool> isSatisfied)
        {
            Target = target;
            IsSatisfied = isSatisfied;
        }
    }
}
