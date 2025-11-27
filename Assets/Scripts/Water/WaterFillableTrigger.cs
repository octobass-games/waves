using Octobass.Waves.Movement;
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

        private GameObject Player;
        private bool IsFillable;

        void Start()
        {
            if (PlayerInput == null)
            {
                Debug.Log("[WaterFillableTrigger]: PlayerInput not set");
            }

            RaiseWaterAction = PlayerInput.actions.FindAction("RaiseWater");
            LowerWaterAction = PlayerInput.actions.FindAction("LowerWater");
        }

        void Update()
        {
            if (IsFillable)
            {
                bool isRaisingWater = RaiseWaterAction.IsPressed();
                bool isLoweringWater = LowerWaterAction.IsPressed();

                if (isRaisingWater || isLoweringWater)
                {
                    if (Player.GetComponent<MovementController>().IsGrounded())
                    {
                        Player.GetComponent<MovementController>().Freeze();
                        Player.GetComponent<Animator>().SetTrigger("IsControllingWater");

                        if (isRaisingWater)
                        {
                            Fillable.Fill();
                        }
                        else
                        {
                            Fillable.Drain();
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    Player.GetComponent<MovementController>().Unfreeze();
                    Player.GetComponent<Animator>().SetTrigger("IsNotControllingWater");
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsFillable = true;
                Player = collision.gameObject;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsFillable = false;
                Player = null;
            }
        }
    }
}
