using Octobass.Waves.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Attack
{
    public class AttackController : MonoBehaviour
    {
        public AttackMove Attack;

        // TODO: if we want multiple attacking states then we need to handle the scenario where the attack is never ended
        private readonly List<CharacterStateId> AttackingStates = new() { CharacterStateId.Grounded };
        private bool IsAttacking;

        void OnActiveFrame()
        {
            Attack.Activate();
        }

        void OnRecoveryFrame()
        {
            EndAttack();
        }

        public AttackSnapshot Tick(CharacterController2DDriverSnapshot driverSnapshot, MovementSnapshot movementSnapshot)
        {
            bool isInAttackingState = AttackingStates.Contains(movementSnapshot.State);

            if (isInAttackingState && driverSnapshot.AttackPressed && !IsAttacking)
            {
                IsAttacking = true;
            }
            else if (!isInAttackingState && IsAttacking)
            {
                EndAttack();
            }

            return new AttackSnapshot(IsAttacking);
        }

        private void EndAttack()
        {
            IsAttacking = false;
            Attack.Deactivate();
        }
    }
}
