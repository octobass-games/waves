using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{
    public string stepSFX;
    public string meleeSFX;
    public string splashSFX;
    public string upgradeSFX;
    public string landSFX;
    public string dashSFX;

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

    void OnUpgrade()
    {
        FMODUnity.RuntimeManager.PlayOneShot(upgradeSFX);
    }

    void IsDiving() //set diving music
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsDiving", 0f);
    }

    void NotDiving() //set standard music
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("IsDiving", 1f);
    }
    void OnLanding()
    {
        FMODUnity.RuntimeManager.PlayOneShot(landSFX);
    }

    void OnDash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(dashSFX);
    }
}
