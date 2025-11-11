using Octobass.Waves.Character;
using UnityEngine;

namespace Octobass.Waves.Item
{
    [CreateAssetMenu]
    public class AbilityItemDefinition : ItemDefinition
    {
        public AbilityDefinition Ability;

        public override ItemInstance ToItemInstance()
        {
            return new AbilityItemInstance(Name, Ability);
        }
    }
}
