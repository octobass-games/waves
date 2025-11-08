using UnityEngine;

namespace Octobass.Waves.Character
{
    public abstract class CharacterController2DDriver : MonoBehaviour
    {
        public abstract CharacterController2DDriverSnapshot TakeSnapshot();

        public abstract void Consume();
    }
}
