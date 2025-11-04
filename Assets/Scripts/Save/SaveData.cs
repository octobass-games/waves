using System;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Save
{
    [Serializable]
    public class SaveData
    {
        [SerializeField]
        private List<SaveEntry> Entries = new();

        public void Add<T>(string key, T data)
        {
            string json = JsonUtility.ToJson(data);
            SaveEntry entry = Entries.Find(entry => entry.key == key);

            if (entry != null)
            {
                entry.value = json;
            }
            else
            {
                Entries.Add(new SaveEntry { key = key, value = json });
            }
        }

        public T Load<T>(string key)
        {
            SaveEntry entry = Entries.Find(entry => entry.key == key);

            return entry != null ? JsonUtility.FromJson<T>(entry.value) : default;
        }
    }
}
