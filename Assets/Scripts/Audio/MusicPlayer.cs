using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using Octobass.Waves.Map;

public class Music : MonoBehaviour
{
    public EventReference mainThemeEvent;
    public EventReference ambientEvent;

    private EventInstance mainInstance;
    private EventInstance ambientInstance;

    [SerializeField] private string MusicState = "MusicState";

    private bool mainPlaying = false;
    private float currentStateValue = 0f;

    void Start()
    {
        PlayTheme();
    }

    public void OnRoomEnter(RoomId roomId)
    {
        SetMusicState(1);
    }

    public void SetMusicState(float value)
    {
        currentStateValue = value;

        if (mainInstance.isValid())
            mainInstance.setParameterByName(MusicState, currentStateValue);

        if (ambientInstance.isValid())
            ambientInstance.setParameterByName(MusicState, currentStateValue);

        if (!mainPlaying)
        {
            PlayTheme();
        }
    }

    void PlayAmbient()
    {
        if (ambientInstance.isValid())
        {
            ambientInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambientInstance.release();
        }

        ambientInstance = RuntimeManager.CreateInstance(ambientEvent);
        ambientInstance.setParameterByName(MusicState, currentStateValue);
        ambientInstance.start();
        mainPlaying = false;
    }

    public void PlayTheme()
    {
        if (mainPlaying)
            return;

        if (ambientInstance.isValid())
        {
            ambientInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambientInstance.release();
        }

        mainInstance = RuntimeManager.CreateInstance(mainThemeEvent);
        mainInstance.setParameterByName(MusicState, currentStateValue);
        mainInstance.start();
        mainPlaying = true;
    }
    void Update()
    {
        if (!mainPlaying || !mainInstance.isValid())
            return;

        PLAYBACK_STATE state;
        mainInstance.getPlaybackState(out state);

        if (state == PLAYBACK_STATE.STOPPED || state == PLAYBACK_STATE.STOPPING)
        {
            mainInstance.release();
            mainPlaying = false;

            PlayAmbient();
        }
    }

    void OnDestroy()
    {
        if (mainInstance.isValid())
        {
            mainInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            mainInstance.release();
        }

        if (ambientInstance.isValid())
        {
            ambientInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            ambientInstance.release();
        }
    }


}
