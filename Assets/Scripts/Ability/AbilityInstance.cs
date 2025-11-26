using Octobass.Waves.Item;
using Octobass.Waves.Movement;
using System;

namespace Octobass.Waves
{
    [Serializable]
    public class AbilityInstance
    {
        public string Name;
        public CharacterStateId NewState;
        public string Explainer;
        public AbilityDefinition Definition;

        public AbilityInstance(string name, CharacterStateId newState, string explainer, AbilityDefinition definition)
        {
            Name = name;
            NewState = newState;
            Explainer = explainer;
            Definition = definition;
        }
    }
}
