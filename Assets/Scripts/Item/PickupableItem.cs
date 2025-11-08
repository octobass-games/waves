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
            Inventory inventory = ServiceLocator.Instance.Get<Inventory>();

            if (inventory != null && inventory.PickUp(ItemDefinition))
            {
                Destroy(gameObject);
            }
        }
    }
}
