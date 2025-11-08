using Octobass.Waves.Attack;

namespace Octobass.Waves.Character
{
    public class AttackStateMachineContext
    {
        public AttackMove Attack;
        public CharacterController2DDriverSnapshot DriverSnapshot;
        public bool IsAttacking;

        public AttackStateMachineContext(AttackMove attackMove, CharacterController2DDriverSnapshot driverSnapshot)
        {
            Attack = attackMove;
            DriverSnapshot = driverSnapshot;
        }
    }
}
