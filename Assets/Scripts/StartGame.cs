using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [FMODUnity.BankRef]
    public List<string> Banks;

    private bool BanksLoaded;
    private bool InputPressed;

    void Start()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl =>
        {
            InputPressed = true;
        });

        foreach (string b in Banks)
        {
            FMODUnity.RuntimeManager.LoadBank(b, true);
        }

        /*
            For Chrome / Safari browsers / WebGL.  Reset audio on response to user interaction (LoadBanks is called from a button press), to allow audio to be heard.
        */
        FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        FMODUnity.RuntimeManager.CoreSystem.mixerResume();

        StartCoroutine(LoadBanks());
    }

    void Update()
    {
        if (BanksLoaded && InputPressed)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator LoadBanks()
    {
        while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }

        BanksLoaded = true;
    }
}
