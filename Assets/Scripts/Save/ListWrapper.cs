using System;
using UnityEngine;

namespace Octobass.Waves.Save
{
    [Serializable]
    public class ListWrapper<T>
    {
        [SerializeReference]
        public T List;

        public ListWrapper(T list)
        {
            List = list;
        }
    }
}
