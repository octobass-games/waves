using UnityEngine;

namespace Octobass.Waves.Item
{
    public class AutoPickUp : MonoBehaviour
    {
        [SerializeField]
        private Animator Animator;

        void Awake()
        {
            if (Animator == null)
            {
                Debug.LogWarning("[AutoPickUp]: Animator not set");
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                Animator.SetTrigger("PickUp");
            }
        }
    }
}
