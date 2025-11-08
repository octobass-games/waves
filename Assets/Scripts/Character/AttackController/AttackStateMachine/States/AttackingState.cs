using UnityEngine;

namespace Octobass.Waves.Character
{
    public class AttackingState : IAttackState
    {
        public AttackStateMachineContext Context;

        private float AttackTime = 200;
        private float AttackTimer;

        public AttackingState(AttackStateMachineContext context)
        {
            Context = context;
        }

        public void Enter()
        {
            AttackTimer = AttackTime;
            Context.IsAttacking = true;
        }

        public void Tick()
        {
            AttackTimer = Mathf.Max(AttackTimer - Time.fixedDeltaTime * 1000, 0f);

            Context.Attack.Activate();

            if (AttackTimer == 0)
            {
                Context.IsAttacking = false;
            }
        }
    }
}
