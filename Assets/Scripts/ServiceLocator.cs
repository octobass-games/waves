using System;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves
{
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set; }

        private Dictionary<Type, object> ServiceRegistry = new();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Register<T>(T service) where T : class
        {
            Type type = typeof(T);

            if (ServiceRegistry.ContainsKey(type))
            {
                Debug.LogWarning($"[ServiceLocator]: Replacing service of type {type.Name}");
            }

            ServiceRegistry[type] = service;
        }

        public void Unregister<T>()
        {
            Type type = typeof(T);

            if (!ServiceRegistry.Remove(type))
            {
                Debug.Log($"[ServiceLocator]: Attempted to remove unregistered service of type {type.Name}");
            }
        }

        public T Get<T>() where T : class
        {
            Type type = typeof(T);

            if (ServiceRegistry.TryGetValue(type, out object service))
            {
                return service as T;
            }

            Debug.LogWarning($"[ServiceLocator]: Could not find service of type {type.Name}");

            return null;
        }
    }
}
