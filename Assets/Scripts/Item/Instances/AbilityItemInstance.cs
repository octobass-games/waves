using Octobass.Waves.Character;
using System;

namespace Octobass.Waves.Item
{
    [Serializable]
    public class AbilityItemInstance : ItemInstance
    {
        public CharacterStateId NewState;

        public AbilityItemInstance(string name, CharacterStateId newState) : base(name)
        {
            NewState = newState;
        }
    }
}
