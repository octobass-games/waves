using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves
{
    public class MovePlayerOffBoat : MonoBehaviour
    {
        public GameObject Player;

        public UnityEvent OnArrival;

        public void MovePlayerOffBoatToWorld()
        {
            Player.transform.parent = null;
            OnArrival.Invoke();
        }
    }
}
