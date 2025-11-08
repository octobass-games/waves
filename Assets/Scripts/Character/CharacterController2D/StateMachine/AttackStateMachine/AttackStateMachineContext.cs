using Octobass.Waves.Attack;

namespace Octobass.Waves.Character
{
    public class AttackStateMachineContext
    {
        public CharacterStateId MovementStateId;
        public AttackMove Attack;
        public CharacterController2DDriverSnapshot DriverSnapshot;
        public bool IsAttacking;

        public AttackStateMachineContext(CharacterStateId movementStateId, AttackMove attackMove, CharacterController2DDriverSnapshot driverSnapshot)
        {
            MovementStateId = movementStateId;
            Attack = attackMove;
            DriverSnapshot = driverSnapshot;
        }
    }
}
