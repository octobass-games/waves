using UnityEngine;

namespace Octobass.Waves.Character
{
    public interface IAttackState
    {
        public void Enter();

        public void Tick();
    }
}
