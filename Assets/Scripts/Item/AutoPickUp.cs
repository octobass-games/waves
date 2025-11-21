using UnityEngine;

namespace Octobass.Waves.Item
{
    public class AutoPickUp : MonoBehaviour
    {
        [SerializeField]
        private PickupableItem PickupableItem;

        void Awake()
        {
            if (PickupableItem == null)
            {
                Debug.LogWarning("[AutoPickUp]: PickupableItem not set");
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                PickupableItem.PickUp();
            }
        }
    }
}
