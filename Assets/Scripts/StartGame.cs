using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        InputSystem.onAnyButtonPress.CallOnce(ctrl => SceneManager.LoadScene("MainMenu"));
    }
}
