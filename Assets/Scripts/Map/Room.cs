using System;

namespace Octobass.Waves.Map
{
    [Serializable]
    public class Room
    {
        public RoomId Id;
        public RoomState State;
        public bool IsShellFound;
        public bool IsTeleporterFound;
    }
}
