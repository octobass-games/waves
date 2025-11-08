using System;

namespace Octobass.Waves.Character
{
    public class AttackStateMachineTransition
    {
        public AttackStateId Target;
        public Func<AttackStateMachineContext, bool> IsSatisfied;

        public AttackStateMachineTransition(AttackStateId target, Func<AttackStateMachineContext, bool> isSatisfied)
        {
            Target = target;
            IsSatisfied = isSatisfied;
        }
    }
}
