using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject CollectablesPanel;
    public GameObject StaffPanel;
    public GameObject MainMenuPanel;
    public GameObject ControlsPanel;
    public GameObject PostcardsPanel;
    private PlayerInput PlayerInput;

    private bool paused = false;
    void Start()
    {
        PlayerInput = new PlayerInput();
        PlayerInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Movement.Pause.WasPerformedThisFrame())
        {
            paused = !paused;
        }

        if (paused)
        {
            OpenPause();
            Time.timeScale = 0;
        }
        else
        {
            ClosePause();
            Time.timeScale = 1;

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


    }

    public void ClosePause()
    {
        MainMenuPanel.SetActive(false);
        CollectablesPanel.SetActive(false);
        StaffPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        PostcardsPanel.SetActive(false);


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

    public void ClickMainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
