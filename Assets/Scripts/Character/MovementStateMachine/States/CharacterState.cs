namespace Octobass.Waves.Character
{
    public abstract class CharacterState
    {
        public virtual void Enter() { }

        public virtual void Tick() { }

        public virtual void Exit() { }
    }
}
