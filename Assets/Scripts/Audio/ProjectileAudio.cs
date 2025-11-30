using UnityEngine;
using FMODUnity;

public class ProjectileAudio : MonoBehaviour
{
    public string explodeSFX;

    void OnExplode()
    {
        RuntimeManager.PlayOneShotAttached(explodeSFX, gameObject);
    }

}
