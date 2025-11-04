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
            if (ServiceLocator.Instance != null)
            {
                SaveManager = ServiceLocator.Instance.Get<SaveManager>();

                if (SaveManager != null)
                {
                    SaveManager.Register(this);
                }
                else
                {
                    Debug.LogWarning("[Saver]: Could not retrieve SaveManager from ServiceLocator");
                }
            }
            else
            {
                Debug.LogWarning("[Saver]: Attempted to get SaveManager but ServiceLocator instance not available");
            }
        }

        private void OnDisable()
        {
            SaveManager?.Unregister(this);
        }
    }
}
