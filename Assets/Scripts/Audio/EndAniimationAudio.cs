using UnityEngine;

public class EndAniimationAudio : MonoBehaviour
{
    public string orbSFX;
    public string orbshineSFX;
    public string orbexpandSFX;
    public string staffgrabSFX;
    public string staffpowerSFX;
    public string staffswingSFX;
    public string staffwaterpowerSFX;
    public string catchSFX;
    public string voiceSFX;
    public string voicetwoSFX;


    void OnOrbHover()
    {
        FMODUnity.RuntimeManager.PlayOneShot(orbSFX);
    }

    void OnOrbShine()
    {
        FMODUnity.RuntimeManager.PlayOneShot(orbshineSFX);
    }

    void OnOrbExpand()
    {
        FMODUnity.RuntimeManager.PlayOneShot(orbexpandSFX);
    }

    void OnStaffGrab()
    {
        FMODUnity.RuntimeManager.PlayOneShot(staffgrabSFX);
    }

    void OnStaffPower()
    {
        FMODUnity.RuntimeManager.PlayOneShot(staffpowerSFX);
    }

    void OnStaffSwing()
    {
        FMODUnity.RuntimeManager.PlayOneShot(staffswingSFX);
    }

    void OnStaffWaterPower()
    {
        FMODUnity.RuntimeManager.PlayOneShot(staffwaterpowerSFX);
    }

    void OnCatch()
    {
        FMODUnity.RuntimeManager.PlayOneShot(catchSFX);
    }

    void OnVoice()
    {
        FMODUnity.RuntimeManager.PlayOneShot(voiceSFX);
    }

    void OnVoiceTwo()
    {
        FMODUnity.RuntimeManager.PlayOneShot(voicetwoSFX);

    }

}
