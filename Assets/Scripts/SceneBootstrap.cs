using Octobass.Waves.Save;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Octobass.Waves
{
    public class SceneBootstrap : MonoBehaviour
    {
        [SerializeField]
        private GameObject Boat;

        [SerializeField]
        private Vector2 BoatFinalPosition;

        [SerializeField]
        private GameObject Player;

        [SerializeField]
        private GameObject OpeningCamera;

        [SerializeField]
        private Animator BoatDriverAnimator;

        [SerializeField]
        private Animator BoatAnimator;

        [SerializeField]
        private Animator BoatBoatAnimator;

        [SerializeField]
        private PlayerInput PlayerInput;

        void Start()
        {
            if (ServiceLocator.Instance != null)
            {
                SaveManager saveManager = ServiceLocator.Instance.Get<SaveManager>();

                if (saveManager != null)
                {
                    if (saveManager.HasSaveData())
                    {
                        OpeningCamera.SetActive(false);
                        BoatDriverAnimator.enabled = false;
                        BoatAnimator.enabled = false;
                        BoatBoatAnimator.enabled = false;
                        Boat.transform.position = BoatFinalPosition;
                        PlayerInput.SwitchCurrentActionMap("Gameplay");

                        saveManager.Load();
                    }
                    else
                    {
                        Player.transform.SetParent(BoatAnimator.transform);
                    }
                }
            }
            else
            {
                Debug.Log("[SceneBootstrap]: Could not find ServiceLocator");
            }
        }
    }
}
