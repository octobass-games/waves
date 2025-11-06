using Octobass.Waves.Save;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Map
{
    public class Cartographer : MonoBehaviour, ISavable
    {
        public UnityEvent<RoomId> OnRoomEntered;
        public UnityEvent<List<Room>, RoomId> OnRoomStateChanged;

        [SerializeField]
        private List<Room> Rooms;

        private RoomId ActiveRoomId = RoomId.E1;

        private const string RoomsSaveKey = "cartographer-rooms";
        private const string ActiveRoomSaveKey = "cartographer-active-room";

        void Start()
        {
            GenerateRoomsFromEnums();
            ServiceLocator.Instance.Register(this);

            OnRoomStateChanged.Invoke(Rooms, RoomId.A4);
        }

        private void GenerateRoomsFromEnums()
        {
            Rooms = new List<Room>();
            var alllIds = Enum.GetValues(typeof(RoomId)).Cast<RoomId>();
            foreach (var item in alllIds)
            {
                var room = new Room();
                room.State = item == RoomId.E1 ? RoomState.Discovered : RoomState.Unknown;
                room.Id = item;
                Rooms.Add(room);
            }
        }

        public void EnterRoom(RoomId roomId)
        {
            Room room = FindRoomById(roomId);

            if (room != null)
            {
                ActiveRoomId = roomId;

                room.State = RoomState.Visited;

                OnRoomEntered.Invoke(ActiveRoomId);
                OnRoomStateChanged.Invoke(Rooms, room.Id);
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        public void EnterPerimeter(RoomId roomId)
        {
            Room room = FindRoomById(roomId);

            if (room != null && room.State != RoomState.Visited)
            {
                room.State = RoomState.Discovered;

                OnRoomStateChanged.Invoke(Rooms, ActiveRoomId);
            }
            else
            {
                Debug.Log($"[Cartographer]: Could not find room with id - {roomId}");
            }
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(RoomsSaveKey, Rooms);
            saveData.Add(ActiveRoomSaveKey, ActiveRoomId);
        }

        public void Load(SaveData saveData)
        {
            Rooms = saveData.Load<List<Room>>(RoomsSaveKey);
            ActiveRoomId = saveData.Load<RoomId>(ActiveRoomSaveKey);
            OnRoomStateChanged.Invoke(Rooms, ActiveRoomId);
        }

        private List<Room> FindRoomsByState(RoomState state)
        {
            return Rooms.FindAll(room => state == room.State);
        }

        private Room FindRoomById(RoomId roomId)
        {
            return Rooms.Find(room => roomId == room.Id);
        }
    }
}
