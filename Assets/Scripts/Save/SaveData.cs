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

            string json = SerializeValue(data);

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

            return entry == null ? default : DeserializeValue<T>(entry);
        }

        private string SerializeValue<T>(T value)
        {
            Type type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return JsonUtility.ToJson(new ListWrapper<T>(value));
            }
            else if (value is bool boolValue)
            {
                return JsonUtility.ToJson(new BoolWrapper(boolValue));
            }
            else if (value is string stringValue)
            {
                return JsonUtility.ToJson(new StringWrapper(stringValue));
            }
            else
            {
                return JsonUtility.ToJson(value);
            }
        }

        private T DeserializeValue<T>(SaveEntry entry)
        {
            Type type = typeof(T);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                return JsonUtility.FromJson<ListWrapper<T>>(entry.value).List;
            }
            else if (type == typeof(bool))
            {
                return (T)(object)JsonUtility.FromJson<BoolWrapper>(entry.value).Value;
            }
            else if (type == typeof(string))
            {
                return (T)(object)JsonUtility.FromJson<StringWrapper>(entry.value).Value;
            }
            else
            {
                return JsonUtility.FromJson<T>(entry.value);
            }
        }
    }
}
