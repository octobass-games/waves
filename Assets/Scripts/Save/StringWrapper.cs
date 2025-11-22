using System;
using UnityEngine;

namespace Octobass.Waves.Save
{
    [Serializable]
    public class StringWrapper
    {
        [SerializeReference]
        public string Value;

        public StringWrapper(string value)
        {
            Value = value;
        }
    }
}
