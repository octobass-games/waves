using UnityEngine;

public class StarterAnimationAudioPlayer : MonoBehaviour
{
    public string music;
    public string writingSFX;
    public string letterSFX;
    public string splashSFX;
    public string surfaceSFX;
    public string bangSFX;
    public string endSplashSfx;

    void OnWriting()
    {
        FMODUnity.RuntimeManager.PlayOneShot(writingSFX);
    }

    void StartMusic()
    {
        FMODUnity.RuntimeManager.PlayOneShot(music);
    }

    void LetterOpen()
    {
        FMODUnity.RuntimeManager.PlayOneShot(letterSFX);
    }

    void OnSplash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(splashSFX);
    }
    void OnSurface()
    {
        FMODUnity.RuntimeManager.PlayOneShot(surfaceSFX);
    }

    void OnBang()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bangSFX);
    }

    void OnEndSplash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(endSplashSfx);
    }
}
