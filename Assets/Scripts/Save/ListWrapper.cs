using System;

namespace Octobass.Waves.Save
{
    [Serializable]
    public class ListWrapper<T>
    {
        public T List;

        public ListWrapper(T list)
        {
            List = list;
        }
    }
}
