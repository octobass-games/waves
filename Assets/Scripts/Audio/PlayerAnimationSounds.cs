using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class PlayerAnimationSounds : MonoBehaviour
{
    public string stepSFX;
    public string meleeSFX;
    public string splashSFX;
    public string upgradeSFX;
    public string landSFX;
    public string dashSFX;
    public string deathSFX;
    public string respawnSFX;
    public string tpSFX;
    public string climbSFX;
    public string floatSFX;

    public EventReference WaterPowerEvent;  

    private EventInstance waterpowerSFX;

    private bool waterPowerIsPlaying = false;

    void Start()
    {
        waterpowerSFX = FMODUnity.RuntimeManager.CreateInstance(WaterPowerEvent);
    }

    void OnStep()
    {
        FMODUnity.RuntimeManager.PlayOneShot(stepSFX);
    }

    void OnFloat()
    {
        FMODUnity.RuntimeManager.PlayOneShot(floatSFX);
    }

    void OnWaterPower()
    {
        if (waterPowerIsPlaying) return;

        waterpowerSFX.start();
        waterPowerIsPlaying = true;
    }

    void StopWaterPower()
    {
        waterpowerSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        waterPowerIsPlaying = false;
    }

    void OnClimb()
    {
        FMODUnity.RuntimeManager.PlayOneShot(climbSFX);
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

    void OnDeath()
	{
		FMODUnity.RuntimeManager.PlayOneShot(deathSFX);
	}

	void OnRespawn()
	{
		FMODUnity.RuntimeManager.PlayOneShot(respawnSFX);
	}

	void OnTP()
	{
		FMODUnity.RuntimeManager.PlayOneShot(tpSFX);
	}

    private void OnDestroy()
    {
        waterpowerSFX.release();
    }

}
