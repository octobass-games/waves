using Octobass.Waves.Map;
using System;
using Unity.Cinemachine;

namespace Octobass.Waves.Camera
{
    [Serializable]
    public struct RoomCameraBinding
    {
        public RoomId Room;
        public CinemachineCamera Camera;
    }
}
