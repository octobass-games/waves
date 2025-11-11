using UnityEngine;

namespace Octobass.Waves.Movement
{
    public class MovementSnapshot
    {
        public readonly CharacterStateId State;
        public readonly Vector2 Displacement;
        public readonly Vector2 FacingDirection;

        public MovementSnapshot(CharacterStateId state, Vector2 displacement, Vector2 facingDirection)
        {
            State = state;
            Displacement = displacement;
            FacingDirection = facingDirection;
        }
    }
}
