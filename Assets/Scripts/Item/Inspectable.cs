using Octobass.Waves.Character;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Octobass.Waves.Item
{
    public class Inspectable : MonoBehaviour
    {
        public UnityEvent OnInspect;

        [SerializeField]
        private UnityEngine.InputSystem.PlayerInput PlayerInput;
        private bool IsInspectable;
        private bool InspectPressed;
        public GameObject DisplayInspect;

        private InputAction InspectAction;

        void Awake()
        {
            if (PlayerInput == null)
            {
                Debug.Log("[Inspectable]: PlayerInput not set");
            }

            InspectAction = PlayerInput.actions.FindAction("Inspect");
        }

        void Update()
        {
            if (InspectAction.WasPressedThisFrame() && IsInspectable)
            {
                InspectPressed = true;
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsInspectable = true;
                if (DisplayInspect != null) {
                DisplayInspect.SetActive(true);
                }
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
                if (DisplayInspect != null)
                {
                    DisplayInspect.SetActive(false);
                }
            }
        }
    }
}
