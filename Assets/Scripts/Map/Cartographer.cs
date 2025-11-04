using Octobass.Waves.Room;
using Octobass.Waves.Save;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class Cartographer : MonoBehaviour, ISavable
    {
        private const string SaveKey = "cartographer";

        private List<RoomInstance> Rooms = new()
        {
            new RoomInstance() { Id = RoomId.A4, State = RoomState.Discovered },
        };

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

        public void Save(SaveData saveData)
        {
            saveData.Add(SaveKey, Rooms);
        }

        public void Load(SaveData saveData)
        {
            foreach (RoomInstance room in saveData.Load<List<RoomInstance>>(SaveKey))
            {
                Debug.Log($"Room: {room.Id}, {room.State}");
            }
        }
    }
}
