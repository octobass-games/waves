using Octobass.Waves.Spawn;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class RoomEntrance : MonoBehaviour
    {
        public RoomId Room;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                Cartographer cartographer = FindFirstObjectByType<Cartographer>();

                if (cartographer != null)
                {
                    cartographer.EnterRoom(Room);
                }
                else
                {
                    Debug.Log("Cartographer not found");
                }
            }
        }
    }
}
