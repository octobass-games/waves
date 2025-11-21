using UnityEngine;

namespace Octobass.Waves.Item
{
    [CreateAssetMenu]
    public class ShellItemDefinition : ItemDefinition
    {
        public override ItemInstance ToItemInstance()
        {
            return new ShellItemInstance(Name);
        }
    }
}
