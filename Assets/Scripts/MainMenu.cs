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
        private GameObject InitiallySelectedGameObject;

        [SerializeField]
        private Button LoadButton;

        [SerializeField]
        private SaveManager SaveManager;

        private GameObject MostRecentlySelectedGameObject;

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

            if (InitiallySelectedGameObject == null)
            {
                Debug.LogWarning("[MainMenu]: InitiallySelectedGameObject not set");
            }

            if (SaveManager.HasSaveData())
            {
                LoadButton.interactable = true;
            }

            EventSystem.current.SetSelectedGameObject(InitiallySelectedGameObject);
        }

        public void NewGame()
        {
            SaveManager.DeleteSaveData();
            SceneManager.LoadScene("OpeningScene");
        }

        public void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }

        void Update()
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
}
