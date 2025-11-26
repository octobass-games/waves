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

        [SerializeField]
        private SaveManager SaveManager;

        void Start()
        {
            if (SaveManager != null)
            {
                if (SaveManager.HasSaveData())
                {
                    OpeningCamera.SetActive(false);
                    BoatDriverAnimator.enabled = false;
                    BoatAnimator.enabled = false;
                    BoatBoatAnimator.enabled = false;
                    Boat.transform.position = BoatFinalPosition;
                    PlayerInput.SwitchCurrentActionMap("Gameplay");

                    SaveManager.Load();
                }
                else
                {
                    Player.transform.SetParent(BoatAnimator.transform);
                }
            }
        }
    }
}
