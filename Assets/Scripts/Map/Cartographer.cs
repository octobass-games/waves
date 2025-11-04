using Octobass.Waves.Room;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class Cartographer : MonoBehaviour
    {
        private List<Room.RoomInstance> Rooms = new();

        public List<Room.RoomInstance> GetDiscoveredRooms()
        {
            return FindRoomsByState(RoomState.Discovered);
        }

        public List<Room.RoomInstance> GetVisitedRooms()
        {
            return FindRoomsByState(RoomState.Visited);
        }

        public void MarkRoomVisited(RoomId roomId)
        {
            Room.RoomInstance room = Rooms.Find(room => room.Id == roomId);

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
            Room.RoomInstance room = Rooms.Find(room => room.Id == roomId);

            if (room != null && room.State != RoomState.Visited)
            {
                room.State = RoomState.Discovered;
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        private List<Room.RoomInstance> FindRoomsByState(RoomState state)
        {
            return Rooms.FindAll(room => state == room.State);
        }
    }
}
