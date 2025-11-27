namespace Octobass.Waves.Attack
{
    public interface IDamageable
    {
        public void OnHit();

        public void OnOneShot();

        public void Reset();
    }
}
