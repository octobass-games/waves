using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class CubeEnemyAudio : MonoBehaviour
{
    public string slimehitSFX;
    public EventReference SleepEvent;

    private EventInstance sleepSFX;

    private bool sleepIsPlaying = false;

    void Start()
    {
        sleepSFX = RuntimeManager.CreateInstance(SleepEvent);
        sleepSFX.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
    }

    void WhenSleeping()
    {
        if (sleepIsPlaying) return;

        sleepSFX.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        sleepSFX.start();
        sleepIsPlaying = true;
    }

    void StopSleeping()
    {
        sleepSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        sleepIsPlaying = false;
    }

    void WhenHit()
    {
        RuntimeManager.PlayOneShot(slimehitSFX);
    }

    void OnDestroy()
    {
        sleepSFX.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        sleepSFX.release();
    }
}




