using UnityEngine;

namespace Octobass.Waves.Save
{
    public class Saver : MonoBehaviour
    {
        private SaveManager SaveManager;

        public void Save(SaveData saveData)
        {
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                savable.Save(saveData);
            }
        }

        public void Load(SaveData saveData)
        {
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                savable.Load(saveData);
            }
        }

        void OnEnable()
        {
            SaveManager = FindFirstObjectByType<SaveManager>();

            if (SaveManager != null)
            {
                SaveManager.Register(this);
            }
            else
            {
                Debug.LogWarning("[Saver]: Could not retrieve SaveManager");
            }
        }

        private void OnDisable()
        {
            SaveManager?.Unregister(this);
        }
    }
}
