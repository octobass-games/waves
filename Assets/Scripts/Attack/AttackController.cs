using Octobass.Waves.Movement;
using Octobass.Waves.Save;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Attack
{
    public class AttackController : MonoBehaviour, ISavable
    {
        [SerializeField]
        private AttackMove RightAttack;

        [SerializeField]
        private AttackMove LeftAttack;

        [SerializeField]
        private GameObject ProjectileAttack;

        [SerializeField]
        private Transform RightProjectileAttackStartPosition;

        [SerializeField]
        private Transform LeftProjectileAttackStartPosition;

        private AttackMove CurrentAttackMove;

        private bool IsProjectileAttackUnlocked;

        private const string SaveKey = "projectile-attack-unlocked";

        private readonly List<CharacterStateId> AttackingStates = new() { CharacterStateId.Grounded, CharacterStateId.Jumping, CharacterStateId.WallJump, CharacterStateId.Falling };
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
            if (IsProjectileAttackUnlocked)
            {
                var go = Instantiate(ProjectileAttack);
                go.GetComponent<Projectile>().Init(FacingDirection, FacingDirection == Vector2.right ? RightProjectileAttackStartPosition.position : LeftProjectileAttackStartPosition.position);
            }
            else
            {
                CurrentAttackMove.Activate();
            }
        }

        public void OnRecoveryFrame()
        {
            EndAttack();
        }

        public AttackSnapshot Tick(MovementDriverSnapshot driverSnapshot, MovementSnapshot movementSnapshot)
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

        public void Save(SaveData saveData)
        {
            saveData.Add(SaveKey, IsProjectileAttackUnlocked);
        }

        public void Load(SaveData saveData)
        {
            IsProjectileAttackUnlocked = saveData.Load<bool>(SaveKey);
        }
    }
}
