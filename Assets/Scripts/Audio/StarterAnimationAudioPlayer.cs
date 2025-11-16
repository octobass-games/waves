using UnityEngine;

public class StarterAnimationAudioPlayer : MonoBehaviour
{
    public string music;
    public string writingSFX;

    void OnWriting()
    {
        FMODUnity.RuntimeManager.PlayOneShot(writingSFX);
    }

    void StartMusic()
    {
        FMODUnity.RuntimeManager.PlayOneShot(music);
    }
}
