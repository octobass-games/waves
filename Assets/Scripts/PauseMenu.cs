using Octobass.Waves.Character;
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
    [SerializeField]
    private UnityEngine.InputSystem.PlayerInput PlayerInput;

    private bool paused = false;
    // TODO: see if there's a way to have this span maps, possible with third map?
    private InputAction PauseAction;

    void Awake()
    {
        if (PlayerInput == null)
        {
            Debug.Log("[PauseMenu]: PlayerInput not set");
        }

        PauseAction = PlayerInput.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseAction.WasPerformedThisFrame())
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

    public void ClickMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
