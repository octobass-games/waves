using Octobass.Waves.Item;
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

        [SerializeField]
        private AbilityDefinition ProjectileAttackAbilityDefinition;

        [SerializeField]
        private AbilityDefinition JumpAbilityDefinition;

        private AttackMove CurrentAttackMove;

        private bool IsNonProjectileAttackUnlocked;
        private bool IsProjectileAttackUnlocked;

        private const string NonProjectileAttackSaveKey = "non-projectile-attack-unlocked";
        private const string ProjectileAttackSaveKey = "projectile-attack-unlocked";

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

        public void OnItemPickedUp(ItemInstance itemInstance)
        {
            if (itemInstance is AbilityItemInstance item)
            {
                if (item.Ability.Definition.Name == JumpAbilityDefinition.Name)
                {
                    IsNonProjectileAttackUnlocked = true;
                }
                else if (item.Ability.Definition.Name == ProjectileAttackAbilityDefinition.Name)
                {
                    IsProjectileAttackUnlocked = true;
                }
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
            if (IsNonProjectileAttackUnlocked || IsProjectileAttackUnlocked)
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
            saveData.Add(NonProjectileAttackSaveKey, IsNonProjectileAttackUnlocked);
            saveData.Add(ProjectileAttackSaveKey, IsProjectileAttackUnlocked);
        }

        public void Load(SaveData saveData)
        {
            IsNonProjectileAttackUnlocked = saveData.Load<bool>(NonProjectileAttackSaveKey);
            IsProjectileAttackUnlocked = saveData.Load<bool>(ProjectileAttackSaveKey);
        }
    }
}
