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

        [SerializeField]
        private GameObject Interact;

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
                        Interact.SetActive(false);

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
                }
                else
                {
                    Interact.SetActive(true);

                    Player.GetComponent<MovementController>().Unfreeze();
                    Player.GetComponent<Animator>().SetTrigger("IsNotControllingWater");
                }
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player) && collision.gameObject.GetComponent<WaterControl>().IsUnlocked)
            {
                IsFillable = true;
                Player = collision.gameObject;
                Interact.SetActive(true);
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                IsFillable = false;
                Player = null;
                Interact.SetActive(false);
            }
        }
    }
}
