namespace Octobass.Waves.CharacterController2D
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
