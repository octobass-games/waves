using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Octobass.Waves.Save
{
    public class SaveManager : MonoBehaviour
    {
        [Tooltip("The name of the JSON file to save to (excluding the .json file extension) when not playing in a browser")]
        public string SaveFileName = "save-data";

        [Tooltip("The name of the property under which to save to when playing in a browser")]
        public string SaveDataKey = "save-data";

        private List<Saver> Savers = new();
        
        private string SaveFilePath;

        void Awake()
        {
            SaveFilePath = Path.Combine(Application.persistentDataPath, SaveFileName + ".json");

            if (ServiceLocator.Instance != null)
            {
                ServiceLocator.Instance.Register(this);
            }
            else
            {
                Debug.LogWarning("[SaveManager]: Attempting to register self with ServiceLocator but instance not available");
            }
        }

        void Start()
        {
            Load();
        }

        public void Register(Saver saver)
        {
            if (!Savers.Contains(saver))
            {
                Savers.Add(saver);
            }
            else
            {
                Debug.Log("[SaveManager]: Attempting to register saver that has already been registered");
            }
        }

        public void Unregister(Saver saver)
        {
            if (Savers.Contains(saver))
            {
                Savers.Remove(saver);
            }
            else
            {
                Debug.Log("[SaveManager]: Attempting to unregister saver that has not been registered");
            }
        }

        public void Save()
        {
            SaveData saveData = new();

            foreach (Saver saver in Savers)
            {
                saver.Save(saveData);
            }

            WriteData(saveData);
        }

        public void Load()
        {
            if (HasSaveData())
            {
                var data = ReadData();

                foreach (Saver saver in Savers)
                {
                    saver.Load(data);
                }
            }
        }

        public void DeleteSaveData()
        {
            if (HasSaveData())
            {
                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    File.Delete(SaveFilePath);
                }
                else
                {
                    PlayerPrefs.DeleteKey(SaveDataKey);
                }
            }
        }

        public bool HasSaveData()
        {
            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                return File.Exists(SaveFilePath);
            }
            else
            {
                return PlayerPrefs.GetString(SaveDataKey) != "";
            }
        }

        private SaveData ReadData()
        {
            if (HasSaveData())
            {
                string json;

                if (Application.platform != RuntimePlatform.WebGLPlayer)
                {
                    using var streamReader = new StreamReader(SaveFilePath);
                    json = streamReader.ReadToEnd();
                }
                else
                {
                    json = PlayerPrefs.GetString(SaveDataKey);
                }

                return JsonUtility.FromJson<SaveData>(json);
            }

            return new SaveData();
        }

        private void WriteData(SaveData data)
        {
            var json = JsonUtility.ToJson(data);

            if (Application.platform != RuntimePlatform.WebGLPlayer)
            {
                using var fileStream = new FileStream(SaveFilePath, FileMode.Create);
                using var streamWriter = new StreamWriter(fileStream);

                streamWriter.Write(json);
            }
            else
            {
                PlayerPrefs.SetString(SaveDataKey, json);
                PlayerPrefs.Save();
            }
        }
    }
}
