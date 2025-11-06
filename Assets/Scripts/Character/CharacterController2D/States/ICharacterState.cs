namespace Octobass.Waves.Character
{
    public interface ICharacterState
    {
        void Enter();

        void Update();

        void FixedUpdate();

        void Exit();

        CharacterStateId? GetTransition();
    }
}
