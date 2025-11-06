using Octobass.Waves.Save;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Map
{
    public class Cartographer : MonoBehaviour, ISavable
    {
        public MapRenderer MapRenderer;

        public UnityEvent<RoomId> OnRoomEntered;
        public UnityEvent<RoomId> OnRoomDiscovered;

        [SerializeField]
        private List<RoomInstance> Rooms;

        private const string RoomsSaveKey = "cartographer-rooms";
        private const string ActiveRoomSaveKey = "cartographer-active-room";

        private RoomId ActiveRoomId = RoomId.E1;

        void Awake()
        {
            if (MapRenderer == null)
            {
                Debug.Log("[Cartographer]: MapRenderer not set");
            }
        }

        public void EnterRoom(RoomId roomId)
        {
            RoomInstance room = FindRoomById(roomId);

            if (room != null)
            {
                room.State = RoomState.Visited;

                OnRoomEntered.Invoke(roomId);
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        public void EnterHallway(RoomId roomId)
        {
            RoomInstance room = FindRoomById(roomId);

            if (room != null && room.State != RoomState.Visited)
            {
                room.State = RoomState.Discovered;
                OnRoomEntered.Invoke(roomId);
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        public void SetActiveRoom(RoomId roomId)
        {
            ActiveRoomId = roomId;
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(RoomsSaveKey, Rooms);
            saveData.Add(ActiveRoomSaveKey, ActiveRoomId);
        }

        public void Load(SaveData saveData)
        {
            Rooms = saveData.Load<List<RoomInstance>>(RoomsSaveKey);
            ActiveRoomId = saveData.Load<RoomId>(ActiveRoomSaveKey);
        }

        private List<RoomInstance> FindRoomsByState(RoomState state)
        {
            return Rooms.FindAll(room => state == room.State);
        }

        private RoomInstance FindRoomById(RoomId roomId)
        {
            return Rooms.Find(room => roomId == room.Id);
        }
    }
}
