using Octobass.Waves.Save;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Octobass.Waves
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject InitiallySelectedButton;

        [SerializeField]
        private Button LoadButton;

        [SerializeField]
        private SaveManager SaveManager;

        [SerializeField]
        private PlayerInput PlayerInput;

        private string CurrentControlScheme;

        void Start()
        {
            if (SaveManager == null)
            {
                Debug.LogWarning("[MainMenu]: SaveManager not set");
            }

            if (LoadButton == null)
            {
                Debug.LogWarning("[MainMenu]: LoadButton not set");
            }

            if (InitiallySelectedButton == null)
            {
                Debug.LogWarning("[MainMenu]: InitiallySelectedButton not set");
            }

            if (SaveManager.HasSaveData())
            {
                LoadButton.interactable = true;
            }

            EventSystem.current.SetSelectedGameObject(InitiallySelectedButton);

            CurrentControlScheme = PlayerInput.currentControlScheme;
        }

        public void NewGame()
        {
            SceneManager.LoadScene("OpeningScene");
        }

        void Update()
        {
            if (CurrentControlScheme != PlayerInput.currentControlScheme)
            {
                if (PlayerInput.currentControlScheme == "Gamepad")
                {
                    EventSystem.current.SetSelectedGameObject(InitiallySelectedButton);
                }

                CurrentControlScheme = PlayerInput.currentControlScheme;
            }
        }
    }
}
