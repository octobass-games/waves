using UnityEngine;

namespace Octobass.Waves.CharacterController2D
{
    public abstract class CharacterDriver : MonoBehaviour
    {
        public abstract DriverSnapshot TakeSnapshot();
    }
}
