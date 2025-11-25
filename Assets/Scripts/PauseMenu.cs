using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Octobass.Waves
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject StaffPanel;

        [SerializeField]
        private GameObject MainMenuPanel;

        [SerializeField]
        private GameObject ControlsPanel;

        [SerializeField]
        private GameObject PostcardsPanel;

        [SerializeField]
        private GameObject InitiallySelectedGameObject;

        [SerializeField]
        private PlayerInput PlayerInput;

        private bool IsPaused = false;

        private InputAction PauseAction;
        private InputActionMap InputActionMapBeforePausing;
        private InputActionMap UiActionMap;

        private GameObject SelectedGameObjectBeforePause;
        private GameObject MostRecentlySelectedGameObject;

        void Awake()
        {
            if (PlayerInput == null)
            {
                Debug.Log("[PauseMenu]: PlayerInput not set");
            }

            UiActionMap = PlayerInput.actions.FindActionMap("UI");

            PlayerInput.actions.FindActionMap("Global").Enable();
            PauseAction = PlayerInput.actions.FindAction("Pause");
        }

        void Update()
        {
            if (IsPaused)
            {
                if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != MostRecentlySelectedGameObject)
                {
                    MostRecentlySelectedGameObject = EventSystem.current.currentSelectedGameObject;
                }
                else if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(MostRecentlySelectedGameObject);
                }

                if (PauseAction.WasPerformedThisFrame())
                {
                    ClosePause();
                    Time.timeScale = 1;
                    IsPaused = false;
                }
            }
            else
            {
                if (PauseAction.WasPerformedThisFrame())
                {
                    InputActionMapBeforePausing = PlayerInput.currentActionMap;
                    OpenPause();
                    Time.timeScale = 0;
                    IsPaused = true;
                }
            }
        }


        public void ClickCollectables()
        {
            MainMenuPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);
        }

        public void OpenPause()
        {
            InputActionMapBeforePausing.Disable();
            UiActionMap.Enable();

            SelectedGameObjectBeforePause = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(InitiallySelectedGameObject);
            MostRecentlySelectedGameObject = EventSystem.current.currentSelectedGameObject;

            MainMenuPanel.SetActive(true);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);
        }

        public void ClosePause()
        {
            UiActionMap.Disable();
            InputActionMapBeforePausing.Enable();

            EventSystem.current.SetSelectedGameObject(SelectedGameObjectBeforePause);
            SelectedGameObjectBeforePause = null;

            MainMenuPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);
        }

        public void ClickStaff()
        {
            MainMenuPanel.SetActive(false);
            StaffPanel.SetActive(true);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);

        }


        public void ClickControls()
        {
            MainMenuPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(true);
            PostcardsPanel.SetActive(false);
        }


        public void ClickPostcards()
        {
            MainMenuPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(true);
        }

        public void ClickRestartRoom()
        {

        }

        public void ClickMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
