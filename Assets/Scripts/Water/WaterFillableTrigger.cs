using UnityEngine;
using UnityEngine.InputSystem;

namespace Octobass.Waves.Water
{
    public class WaterFillableTrigger : MonoBehaviour
    {
        [SerializeField]
        private WaterFillable Fillable;

        [SerializeField]
        private PlayerInput PlayerInput;

        private InputAction RaiseWaterAction;
        private InputAction LowerWaterAction;
        
        private bool IsFillable;

        void Awake()
        {
            if (PlayerInput == null)
            {
                Debug.Log("[WaterFillableTrigger]: PlayerInput not set");
            }

            RaiseWaterAction = PlayerInput.actions.FindAction("Inspect");
            LowerWaterAction = PlayerInput.actions.FindAction("Attack");
        }

        void Update()
        {
            if (IsFillable)
            {
                if (RaiseWaterAction.IsPressed())
                {
                    Fillable.Fill();
                }
                else if (LowerWaterAction.IsPressed())
                {
                    Fillable.Drain();
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsFillable = true;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsFillable = false;
            }
        }
    }
}
