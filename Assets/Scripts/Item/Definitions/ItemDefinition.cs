using UnityEngine;

namespace Octobass.Waves.Item
{
    public class ItemDefinition : ScriptableObject
    {
        public string Name;

        public virtual ItemInstance ToItemInstance()
        {
            return new ItemInstance(Name);
        }
    }
}
