using Octobass.Waves.Movement;
using UnityEngine;

namespace Octobass.Waves
{
    [CreateAssetMenu]
    public class AbilityDefinition : ScriptableObject
    {
        public CharacterStateId NewState;
        public string Name;
        public string Explainer;
    }
}
