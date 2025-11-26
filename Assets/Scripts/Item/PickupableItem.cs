using UnityEngine;

namespace Octobass.Waves.Item
{
    public class PickupableItem : MonoBehaviour
    {
        public ItemDefinition ItemDefinition;

        void Awake()
        {
            if (ItemDefinition == null)
            {
                Debug.LogWarning("[PickupableItem]: ItemDefinition not set");
            }
        }

        public void PickUp()
        {
            Inventory inventory = FindFirstObjectByType<Inventory>();

            if (inventory != null && inventory.PickUp(ItemDefinition))
            {
                Destroy(gameObject);
            }
        }
    }
}
