using Octobass.Waves.Save;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves
{
    public class OneShotEntryTrigger : MonoBehaviour, ISavable
    {
        [SerializeField]
        private string Name;

        [SerializeField]
        private UnityEvent OnEnter;

        private bool HasTriggered;
        private string SaveKey;

        void Awake()
        {
            SaveKey = $"one-shot-entry-trigger:{Name}";
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player) && !HasTriggered)
            {
                OnEnter.Invoke();

                HasTriggered = true;
            }
        }

        public void Save(SaveData saveData)
        {
            saveData.Add<bool>(SaveKey, HasTriggered);
        }

        public void Load(SaveData saveData)
        {
            HasTriggered = saveData.Load<bool>(SaveKey);

            if (HasTriggered)
            {
                OnEnter.Invoke();
            }
        }
    }
}
