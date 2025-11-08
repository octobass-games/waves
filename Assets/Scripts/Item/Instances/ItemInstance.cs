using System;

namespace Octobass.Waves.Item
{
    [Serializable]
    public class ItemInstance
    {
        public string Name;

        public ItemInstance(string name)
        {
            Name = name;
        }
    }
}
