using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningAnimation : MonoBehaviour
{
    public GameObject FadeCanvas;
    public void openingEnd()
    {
        FadeCanvas.SetActive(true);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Game");
    }
}
