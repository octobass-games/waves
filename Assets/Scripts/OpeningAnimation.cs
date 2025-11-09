using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningAnimation : MonoBehaviour
{
    void openingEnd()
    {
        SceneManager.LoadScene("Game");
    }
}
