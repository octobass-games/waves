using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Octobass.Waves
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject CollectablesPanel;

        [SerializeField]
        private GameObject StaffPanel;

        [SerializeField]
        private GameObject MainMenuPanel;

        [SerializeField]
        private GameObject ControlsPanel;

        [SerializeField]
        private GameObject PostcardsPanel;

        [SerializeField]
        private PlayerInput PlayerInput;

        private bool paused = false;
        private InputAction PauseAction;
        private InputActionMap GameplayActionMap;
        private InputActionMap UiActionMap;

        void Awake()
        {
            if (PlayerInput == null)
            {
                Debug.Log("[PauseMenu]: PlayerInput not set");
            }

            GameplayActionMap = PlayerInput.actions.FindActionMap("Gameplay");
            UiActionMap = PlayerInput.actions.FindActionMap("UI");

            PlayerInput.actions.FindActionMap("Global").Enable();
            PauseAction = PlayerInput.actions.FindAction("Pause");
        }

        void Update()
        {
            if (PauseAction.WasPerformedThisFrame())
            {
                if (!paused)
                {
                    OpenPause();
                    Time.timeScale = 0;
                    paused = true;
                }
                else
                {
                    ClosePause();
                    Time.timeScale = 1;
                    paused = false;
                }
            }
        }


        public void ClickCollectables()
        {
            MainMenuPanel.SetActive(false);
            CollectablesPanel.SetActive(true);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);
        }

        public void OpenPause()
        {
            MainMenuPanel.SetActive(true);
            CollectablesPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);

            GameplayActionMap.Disable();
            UiActionMap.Enable();
        }

        public void ClosePause()
        {
            MainMenuPanel.SetActive(false);
            CollectablesPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);
            
            GameplayActionMap.Enable();
            UiActionMap.Disable();
        }

        public void ClickStaff()
        {
            MainMenuPanel.SetActive(false);
            CollectablesPanel.SetActive(false);
            StaffPanel.SetActive(true);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);

        }


        public void ClickControls()
        {
            MainMenuPanel.SetActive(false);
            CollectablesPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(true);
            PostcardsPanel.SetActive(false);

        }


        public void ClickPostcards()
        {
            MainMenuPanel.SetActive(false);
            CollectablesPanel.SetActive(false);
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
