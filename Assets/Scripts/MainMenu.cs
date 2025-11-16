using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject InitiallySelectedButton;

    public void NewGame()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(InitiallySelectedButton);
    }
}
