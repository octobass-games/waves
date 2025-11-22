using Octobass.Waves.Save;
using UnityEngine;

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

        void Start()
        {
            if (ServiceLocator.Instance != null)
            {
                SaveManager saveManager = ServiceLocator.Instance.Get<SaveManager>();

                if (saveManager != null && saveManager.HasSaveData())
                {
                    OpeningCamera.SetActive(false);
                    BoatDriverAnimator.enabled = false;
                    BoatAnimator.enabled = false;
                    BoatBoatAnimator.enabled = false;
                    Boat.transform.position = BoatFinalPosition;
                    Player.transform.parent = null;

                    saveManager.Load();
                }
            }
            else
            {
                Debug.Log("[SceneBootstrap]: Could not find ServiceLocator");
            }
        }
    }
}
