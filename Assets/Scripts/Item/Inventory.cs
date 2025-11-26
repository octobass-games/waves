using Octobass.Waves.Save;
using System;
using System.Collections.Generic;
using System.Linq;
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
            else if (item is ShellItemDefinition shellItemDefinition)
            {
                if (Items.Find(inventoryItem => inventoryItem.Name == item.Name) == null)
                {
                    if (shellItemDefinition.ToItemInstance() is ShellItemInstance instance)
                    {
                        Items.Add(instance);
                        OnItemPickedUp.Invoke(instance);
                    }
                    else
                    {
                        Debug.LogWarning("[Inventory]: ShellItemDefinition return instance other than ShellItemInstance");
                    }
                }
                else
                {
                    Debug.Log("[Inventory]: Attempting to add duplicate shell item to inventory");
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

            List<PickupableItem> pickupableItems = FindObjectsByType<PickupableItem>(FindObjectsSortMode.None).ToList();

            foreach (ItemInstance item in Items)
            {
                PickupableItem pickupableItem = pickupableItems.Find(pickupableItem => pickupableItem.ItemDefinition.Name == item.Name);

                if (pickupableItem != null)
                {
                    Destroy(pickupableItem.gameObject);
                }
                else
                {
                    Debug.Log($"[Inventory]: PickupableItem not found for {item.Name}");
                }
            }
        }

        public ItemInstance FindItem(ItemDefinition item)
        {
            return Items.Find(i => i.Name == item.Name);
        }

        public List<AbilityItemInstance> GetAbilityItems()
        {
            return Items.OfType<AbilityItemInstance>().ToList();
        }
    }
}
