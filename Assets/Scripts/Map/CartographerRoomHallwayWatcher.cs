using Octobass.Waves.Room;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class CartographerRoomHallwayWatcher : MonoBehaviour, IRoomHallwayWatcher
    {
        public RoomId RoomId;
        public Cartographer Cartographer;

        void Awake()
        {
            if (Cartographer == null)
            {
                Debug.LogWarning("[CartographerRoomEntranceWatcher]: Cartographer not set");
            }
        }

        public void OnEntrance()
        {
            Cartographer.MarkRoomDiscovered(RoomId);
        }
    }
}
