namespace Octobass.Waves.Attack
{
    public class AttackSnapshot
    {
        public bool IsAttacking;
        public bool IsProjectileAttacking;

        public AttackSnapshot(bool isAttacking, bool isProjectileAttacking)
        {
            IsAttacking = isAttacking;
            IsProjectileAttacking = isProjectileAttacking;  
        }
    }
}
