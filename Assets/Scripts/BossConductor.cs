using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.U2D;

public class BossConductor : MonoBehaviour
{
    public CinemachineCamera Camera;
    public Transform Boss;
   public void StartBattle()
    {
        Camera.Follow = Boss;
    }
}
