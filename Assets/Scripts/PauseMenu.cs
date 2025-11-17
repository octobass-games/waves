using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject CollectablesPanel;
    public GameObject StaffPanel;
    public GameObject MainMenuPanel;
    public GameObject ControlsPanel;
    public GameObject PostcardsPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        SceneManager.LoadScene("MainMenu");
    }
}
