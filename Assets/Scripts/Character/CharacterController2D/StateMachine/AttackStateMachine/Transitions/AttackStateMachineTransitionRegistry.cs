using System.Collections.Generic;

namespace Octobass.Waves.Character
{
    public static class AttackStateMachineTransitionRegistry
    {
        public static Dictionary<AttackStateId, List<AttackStateMachineTransition>> Transitions = new()
        {
            {
                AttackStateId.Passive, new()
                {
                    new(AttackStateId.Attacking, (AttackStateMachineContext context) => context.DriverSnapshot.AttackPressed)
                }
            },
            {
                AttackStateId.Attacking, new()
                {
                    new(AttackStateId.Passive, (AttackStateMachineContext context) => !context.IsAttacking)
                }
            }
        };
    }
}
