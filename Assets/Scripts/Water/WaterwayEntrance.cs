using Octobass.Waves.Attack;
using Octobass.Waves.Movement;
using UnityEngine;

namespace Octobass.Waves.Water
{
    public class WaterwayEntrance : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player) && !collision.GetComponent<MovementController>().CanSwim())
            {
                collision.GetComponent<IDamageable>().OnOneShot();
            }
        }
    }
}
