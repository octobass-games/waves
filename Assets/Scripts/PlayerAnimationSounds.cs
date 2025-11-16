using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{
    public string stepSFX;
    public string meleeSFX;
    public string splashSFX;

    void OnStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(stepSFX);
    }

    void OnMelee()
    {
        FMODUnity.RuntimeManager.PlayOneShot(meleeSFX);
    }

    void OnSplash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(splashSFX);
    }
}
