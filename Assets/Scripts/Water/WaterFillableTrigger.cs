using UnityEngine;

namespace Octobass.Waves.Water
{
    public class WaterFillableTrigger : MonoBehaviour
    {
        [SerializeField]
        private WaterFillable Fillable;

        private bool IsFillable;
        private PlayerInput PlayerInput;

        void Awake()
        {
            PlayerInput = new PlayerInput();
            PlayerInput.Enable();
        }

        void Update()
        {
            if (IsFillable)
            {
                if (PlayerInput.Movement.Inspect.IsPressed())
                {
                    Fillable.Fill();
                }
                else if (PlayerInput.Movement.Attack.IsPressed())
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
