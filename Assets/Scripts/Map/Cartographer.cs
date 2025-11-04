using Octobass.Waves.Room;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class Cartographer : MonoBehaviour
    {
        private List<RoomInstance> Rooms = new();

        public List<RoomInstance> GetDiscoveredRooms()
        {
            return FindRoomsByState(RoomState.Discovered);
        }

        public List<RoomInstance> GetVisitedRooms()
        {
            return FindRoomsByState(RoomState.Visited);
        }

        public void MarkRoomVisited(RoomId roomId)
        {
            RoomInstance room = Rooms.Find(room => room.Id == roomId);

            if (room != null)
            {
                room.State = RoomState.Visited;
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        public void MarkRoomDiscovered(RoomId roomId)
        {
            RoomInstance room = Rooms.Find(room => room.Id == roomId);

            if (room != null && room.State != RoomState.Visited)
            {
                room.State = RoomState.Discovered;
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        private List<RoomInstance> FindRoomsByState(RoomState state)
        {
            return Rooms.FindAll(room => state == room.State);
        }
    }
}
