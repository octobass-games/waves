using UnityEngine;

public class AudioOneShot : MonoBehaviour
{
    public string SFX;
   
    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(SFX);
    }
    
}


