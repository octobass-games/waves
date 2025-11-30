using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public string oneshotSFX;

    public void PlaySFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(oneshotSFX);
    }
}
