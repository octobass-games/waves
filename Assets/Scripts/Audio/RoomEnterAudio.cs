using UnityEngine;
using Octobass.Waves.Map;

public class RoomEnterAudio : MonoBehaviour
{

    public string roomSFX;

    public void OnRoomEnter(RoomId roomId)
    {
        FMODUnity.RuntimeManager.PlayOneShot(roomSFX);
    }
}
