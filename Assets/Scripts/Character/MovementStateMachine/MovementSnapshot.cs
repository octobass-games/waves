using UnityEngine;

namespace Octobass.Waves.Character
{
    public class MovementSnapshot
    {
        public readonly CharacterStateId State;
        public readonly Vector2 Displacement;

        public MovementSnapshot(CharacterStateId state, Vector2 displacement)
        {
            State = state;
            Displacement = displacement;
        }
    }
}
