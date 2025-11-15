using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{
    public string stepSFX;

    void OnStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(stepSFX);
    }
}
