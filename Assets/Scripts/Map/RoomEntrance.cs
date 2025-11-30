using Octobass.Waves.Spawn;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class RoomEntrance : MonoBehaviour
    {
        public RoomId Room;

        [SerializeField]
        private BoxCollider2D EntranceCollider;

        void Start()
        {
            EntranceCollider = GetComponent<BoxCollider2D>();

            RaycastHit2D[] results = new RaycastHit2D[10];

            var Player = FindFirstObjectByType<SpawnTracker>().gameObject;

            int count = EntranceCollider.Cast(Vector2.right, results, Player.GetComponent<BoxCollider2D>().size.x);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RaycastHit2D hit = results[i];

                    if (hit.collider != null && hit.collider.gameObject.GetComponent<RoomEntrance>() != null)
                    {
                        Debug.Log($"{gameObject.name} is colliding with {hit.collider.gameObject.name}");
                    }
                }
            }
            
            count = EntranceCollider.Cast(Vector2.left, results, Player.GetComponent<BoxCollider2D>().size.x);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RaycastHit2D hit = results[i];

                    if (hit.collider != null && hit.collider.gameObject.GetComponent<RoomEntrance>() != null)
                    {
                        Debug.Log($"{gameObject.name} is colliding with {hit.collider.gameObject.name}");
                    }
                }
            }

            count = EntranceCollider.Cast(Vector2.up, results, Player.GetComponent<BoxCollider2D>().size.y);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RaycastHit2D hit = results[i];

                    if (hit.collider != null && hit.collider.gameObject.GetComponent<RoomEntrance>() != null)
                    {
                        Debug.Log($"{gameObject.name} is colliding with {hit.collider.gameObject.name}");
                    }
                }
            }

            count = EntranceCollider.Cast(Vector2.down, results, Player.GetComponent<BoxCollider2D>().size.y);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    RaycastHit2D hit = results[i];

                    if (hit.collider != null && hit.collider.gameObject.GetComponent<RoomEntrance>() != null)
                    {
                        Debug.Log($"{gameObject.name} is colliding with {hit.collider.gameObject.name}");
                    }
                }
            }
        }

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
