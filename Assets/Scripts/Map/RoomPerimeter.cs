using UnityEngine;

namespace Octobass.Waves.Map
{
    public class RoomPerimeter : MonoBehaviour
    {
        public RoomId Room;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                Cartographer cartographer = ServiceLocator.Instance.Get<Cartographer>();

                if (cartographer != null)
                {
                    cartographer.EnterPerimeter(Room);
                }
            }
        }
    }
}
