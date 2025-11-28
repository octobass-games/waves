using UnityEngine;

namespace Octobass.Waves.Movement
{
    public abstract class CharacterState
    {
        public virtual void Enter(CharacterStateId previousStateId, MovementDriverSnapshot movementDriverSnapshot) { }

        public abstract StateSnapshot Tick(StateSnapshot previousSnapshot, MovementDriverSnapshot driverSnapshot, Vector2 facingDirection);

        public virtual void Exit() { }
    }
}
