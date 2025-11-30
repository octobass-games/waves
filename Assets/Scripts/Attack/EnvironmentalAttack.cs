using UnityEngine;

namespace Octobass.Waves.Attack
{
    public class EnvironmentalAttack : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                if (collision.TryGetComponent(out IDamageable damageable))
                {
                    damageable.OnOneShot();
                }
                else
                {
                    Debug.Log("[EnironmentalAttack]: Could not find IDamageable on player");
                }
            }
        }
    }
}
