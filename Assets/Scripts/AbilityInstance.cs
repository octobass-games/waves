using Octobass.Waves.Character;
using System;

namespace Octobass.Waves
{
    [Serializable]
    public class AbilityInstance
    {
        public string Name;
        public CharacterStateId NewState;
        public string Explainer;

        public AbilityInstance(string name, CharacterStateId newState, string explainer)
        {
            Name = name;
            NewState = newState;
            Explainer = explainer;
        }
    }
}
