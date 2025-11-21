using Octobass.Waves.Save;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Item
{
    public class Inventory : MonoBehaviour, ISavable
    {
        public UnityEvent<ItemInstance> OnItemPickedUp;

        [SerializeField]
        [Tooltip("A registry of all items")]
        private List<ItemDefinition> ItemRegistry;

        [SerializeReference]
        private List<ItemInstance> Items = new();

        private const string ItemsSaveKey = "inventory-items";

        void Awake()
        {
            ServiceLocator.Instance.Register(this);
        }

        public bool PickUp(ItemDefinition item)
        {
            if (item is AbilityItemDefinition abilityItemDefintion)
            {
                if (Items.Find(inventoryItem => inventoryItem.Name == item.Name) == null)
                {
                    if (abilityItemDefintion.ToItemInstance() is AbilityItemInstance instance)
                    {
                        Items.Add(instance);
                        OnItemPickedUp.Invoke(instance);
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
            else if (item is LoreItemDefinition loreItemDefintion)
            {
                if (Items.Find(inventoryItem => inventoryItem.Name == item.Name) == null)
                {
                    if (loreItemDefintion.ToItemInstance() is LoreItemInstance instance)
                    {
                        Items.Add(instance);
                        OnItemPickedUp.Invoke(instance);
                    }
                    else
                    {
                        Debug.LogWarning("[Inventory]: LoreItemDefinition return instance other than LoreItemInstance");
                    }
                }
                else
                {
                    Debug.Log("[Inventory]: Attempting to add duplicate lore item to inventory");
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
