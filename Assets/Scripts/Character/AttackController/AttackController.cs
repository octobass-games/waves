using Octobass.Waves.Attack;
using UnityEngine;

namespace Octobass.Waves.Character
{
    public class AttackController : MonoBehaviour
    {
        public AttackMove Attack;

        private bool IsAttacking;

        void OnActiveFrame()
        {
            Attack.Activate();
        }

        void OnRecoveryFrame()
        {
            IsAttacking = false;
            Attack.Deactivate();
        }

        public AttackSnapshot Tick(CharacterController2DDriverSnapshot driverSnapshot, MovementSnapshot movementSnapshot)
        {
            if (driverSnapshot.AttackPressed && !IsAttacking)
            {
                IsAttacking = true;
            }

            return new AttackSnapshot(IsAttacking);
        }
    }
}
