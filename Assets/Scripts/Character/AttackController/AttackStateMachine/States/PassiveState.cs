namespace Octobass.Waves.Character
{
    public class PassiveState : IAttackState
    {
        public AttackStateMachineContext Context;

        public PassiveState(AttackStateMachineContext context)
        {
            Context = context;
        }

        public void Enter() {}

        public void Tick() {}
    }
}
