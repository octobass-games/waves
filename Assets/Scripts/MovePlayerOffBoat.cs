using UnityEngine;

public class MovePlayerOffBoat : MonoBehaviour
{
    public GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayerOffBoatToWorld()
    {
        Player.transform.parent = null;
    }
}
