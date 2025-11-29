using Octobass.Waves.Item;
using Octobass.Waves.Map;
using TMPro;
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

        [SerializeField]
        private TextMeshProUGUI ShellCount;

        [SerializeField]
        private TextMeshProUGUI TeleporterCount;

        [SerializeField]
        private TextMeshProUGUI LoreCount;

        [SerializeField]
        private Cartographer Cartographer;

        [SerializeField]
        private Inventory Inventory;

        private bool IsPaused = false;

        private InputAction PauseAction;
        private InputActionMap InputActionMapBeforePausing;
        private InputActionMap UiActionMap;

        private GameObject SelectedGameObjectBeforePause;
        private GameObject MostRecentlySelectedGameObject;

        void Start()
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
            PlayerInput.SwitchCurrentActionMap(UiActionMap.name);

            SelectedGameObjectBeforePause = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.SetSelectedGameObject(InitiallySelectedGameObject);
            MostRecentlySelectedGameObject = EventSystem.current.currentSelectedGameObject;

            ShellCount.text = $"{Cartographer.GetFoundShellCount()}/48";
            TeleporterCount.text = $"{Cartographer.GetFoundTeleporterCount()}/48";
            LoreCount.text = $"{Inventory.GetLoreItemCount()}/10";

            MainMenuPanel.SetActive(true);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);
        }

        public void ClosePause()
        {
            PlayerInput.SwitchCurrentActionMap(InputActionMapBeforePausing.name);

            EventSystem.current.SetSelectedGameObject(SelectedGameObjectBeforePause);
            SelectedGameObjectBeforePause = null;

            MainMenuPanel.SetActive(false);
            StaffPanel.SetActive(false);
            ControlsPanel.SetActive(false);
            PostcardsPanel.SetActive(false);

            Time.timeScale = 1;
            IsPaused = false;
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
