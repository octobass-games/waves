using Octobass.Waves.Save;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Item
{
    public class Inventory : MonoBehaviour, ISavable
    {
        public UnityEvent<AbilityItemInstance> OnAbilityItemPickedUp;

        [SerializeReference]
        private List<ItemInstance> Items = new();

        private const string ItemsSaveKey = "inventory-items";

        void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        public bool PickUp(ItemDefinition item)
        {
            if (item is AbilityItemDefinition definition)
            {
                if (Items.Find(inventoryItem => inventoryItem.Name == item.Name) == null)
                {
                    if (definition.ToItemInstance() is AbilityItemInstance instance)
                    {
                        Items.Add(instance);
                        OnAbilityItemPickedUp.Invoke(instance);
                    }
                    else
                    {
                        Debug.LogWarning("[Inventory]: AbilityItemDefinition return instance other than AbilityItemInstance");
                    }
                }
                else
                {
                    Debug.Log("[Inventory]: Attempting to add duplicate ability item to inventory");
                }

                return true;
            }

            Debug.Log("[Inventory]: Attempting to add unsupported item type to inventory");

            return false;
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(ItemsSaveKey, Items);
        }

        public void Load(SaveData saveData)
        {
            Items = saveData.Load<List<ItemInstance>>(ItemsSaveKey);
        }
    }
}
