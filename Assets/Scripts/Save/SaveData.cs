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
            SaveEntry entry = Entries.Find(entry => entry.key == key);

            Type type = typeof(T);
            string json = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)
                ? JsonUtility.ToJson(new ListWrapper<T>(data))
                : JsonUtility.ToJson(data);

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

            if (entry == null)
            {
                return default;
            }
            else
            {
                Type type = typeof(T);

                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)
                    ? JsonUtility.FromJson<ListWrapper<T>>(entry.value).List
                    : JsonUtility.FromJson<T>(entry.value);
            }
        }
    }
}
