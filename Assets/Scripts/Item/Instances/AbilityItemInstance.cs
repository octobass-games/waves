using Octobass.Waves.Character;
using System;

namespace Octobass.Waves.Item
{
    [Serializable]
    public class AbilityItemInstance : ItemInstance
    {
        public AbilityInstance Ability;

        public AbilityItemInstance(string name, AbilityDefinition abilityDefinition) : base(name)
        {
            Ability = new AbilityInstance(abilityDefinition.name, abilityDefinition.NewState, abilityDefinition.Explainer);
        }
    }
}
