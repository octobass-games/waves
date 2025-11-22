using System;
using UnityEngine;

namespace Octobass.Waves.Save
{
    [Serializable]
    public class BoolWrapper
    {
        [SerializeReference]
        public bool Value;

        public BoolWrapper(bool value)
        {
            Value = value;
        }
    }
}
