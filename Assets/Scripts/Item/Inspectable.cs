using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Item
{
    public class Inspectable : MonoBehaviour
    {
        public UnityEvent OnInspect;

        private PlayerInput PlayerInput;
        private bool IsInspectable;
        private bool InspectPressed;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();
        }

        void Update()
        {
            if (PlayerInput.Movement.Inspect.WasPressedThisFrame() && IsInspectable)
            {
                InspectPressed = true;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsInspectable = true;
            }
        }

        void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player) && InspectPressed)
            {
                InspectPressed = false;
                OnInspect.Invoke();
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsInspectable = false;
            }
        }
    }
}
