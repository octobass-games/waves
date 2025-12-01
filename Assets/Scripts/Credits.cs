using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private GameObject ContinueButton;

    void Update()
    {
        if (EventSystem.current == null || EventSystem.current.currentSelectedGameObject != ContinueButton)
        {
            EventSystem.current.SetSelectedGameObject(ContinueButton);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
