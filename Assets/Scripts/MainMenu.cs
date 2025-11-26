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
        private GameObject NewGameButton;

        [SerializeField]
        private Button LoadButton;

        [SerializeField]
        private GameObject QuitButton;

        [SerializeField]
        private SaveManager SaveManager;

        private GameObject MostRecentlySelectedGameObject;

        private bool ButtonsEnabled;

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

            if (NewGameButton == null)
            {
                Debug.LogWarning("[MainMenu]: NewGameButton not set");
            }

            if (QuitButton == null)
            {
                Debug.LogWarning("[MainMenu]: QuitButton not set");
            }

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                QuitButton.SetActive(false);
            }
        }

        public void NewGame()
        {
            if (ButtonsEnabled)
            {
                SaveManager.DeleteSaveData();
                SceneManager.LoadScene("OpeningScene");
            }
        }

        public void LoadGame()
        {
            if (ButtonsEnabled)
            {
                SceneManager.LoadScene("Game");
            }
        }

        public void Credits()
        {
            if (ButtonsEnabled)
            {
                SceneManager.LoadScene("Credits");
            }
        }

        public void Quit()
        {
            if (ButtonsEnabled)
            {
                Application.Quit();
            }
        }

        void Update()
        {
            if (ButtonsEnabled)
            {
                if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != MostRecentlySelectedGameObject)
                {
                    MostRecentlySelectedGameObject = EventSystem.current.currentSelectedGameObject;
                }
                else if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(MostRecentlySelectedGameObject);
                }
            }
        }

        public void EnableButtons()
        {
            ButtonsEnabled = true;

            if (SaveManager.HasSaveData())
            {
                LoadButton.interactable = true;
                EventSystem.current.SetSelectedGameObject(LoadButton.gameObject);
            }
            else
            {
                LoadButton.interactable = false;
                EventSystem.current.SetSelectedGameObject(NewGameButton.gameObject);
            }
        }
    }
}
