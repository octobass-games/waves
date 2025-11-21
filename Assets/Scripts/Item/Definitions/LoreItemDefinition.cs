using UnityEngine;

namespace Octobass.Waves.Item
{
    [CreateAssetMenu]
    public class LoreItemDefinition : ItemDefinition
    {
        [TextArea]
        public string Text;
        public override ItemInstance ToItemInstance()
        {
            return new LoreItemInstance(Name, this);
        }
    }
}
