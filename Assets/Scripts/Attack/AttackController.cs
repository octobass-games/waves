using Octobass.Waves.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Attack
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField]
        private AttackMove RightAttack;

        [SerializeField]
        private AttackMove LeftAttack;

        private AttackMove CurrentAttackMove;

        // TODO: if we want multiple attacking states then we need to handle the scenario where the attack is never ended
        private readonly List<CharacterStateId> AttackingStates = new() { CharacterStateId.Grounded };
        private bool IsAttacking;
        private Vector2 FacingDirection;

        void Awake()
        {
            if (RightAttack == null)
            {
                Debug.Log("[AttackController]: RightAttack not set");
            }

            if (LeftAttack == null)
            {
                Debug.Log("[AttackController]: LeftAttack not set");
            }
        }

        void OnActiveFrame()
        {
            CurrentAttackMove.Activate();
        }

        public void OnRecoveryFrame()
        {
            EndAttack();
        }

        public AttackSnapshot Tick(CharacterController2DDriverSnapshot driverSnapshot, MovementSnapshot movementSnapshot)
        {
            bool changedFacingDirection = FacingDirection != movementSnapshot.FacingDirection;

            if (changedFacingDirection)
            {
                if (IsAttacking)
                {
                    EndAttack();
                }

                if (movementSnapshot.FacingDirection == Vector2.right)
                {
                    CurrentAttackMove = RightAttack;
                    RightAttack.gameObject.SetActive(true);
                    LeftAttack.gameObject.SetActive(false);
                }
                else
                {
                    CurrentAttackMove = LeftAttack;
                    RightAttack.gameObject.SetActive(false);
                    LeftAttack.gameObject.SetActive(true);
                }

                FacingDirection = movementSnapshot.FacingDirection;
            }

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
            CurrentAttackMove.Deactivate();
        }
    }
}
