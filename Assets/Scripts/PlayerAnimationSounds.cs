using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{
    public string stepSFX;
    public string meleeSFX;

    void OnStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(stepSFX);
    }

    void OnMelee()
    {
        FMODUnity.RuntimeManager.PlayOneShot(meleeSFX);
    }
}
