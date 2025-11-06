namespace Octobass.Waves.Character
{
    public interface ICharacterState
    {
        void Enter();

        void Tick();

        void Exit();

        CharacterStateId? GetTransition();
    }
}
