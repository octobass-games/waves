using UnityEngine;

namespace Octobass.Waves.Movement
{
    public abstract class CharacterState
    {
        public virtual void Enter(CharacterStateId previousStateId) { }

        public abstract StateSnapshot Tick(StateSnapshot previousSnapshot, CharacterController2DDriverSnapshot driverSnapshot, Vector2 facingDirection);

        public virtual void Exit() { }
    }
}
