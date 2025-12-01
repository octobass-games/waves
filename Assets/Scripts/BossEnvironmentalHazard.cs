using Octobass.Waves.Attack;
using Octobass.Waves;
using UnityEngine;

public class BossEnvironmentalHazard : MonoBehaviour
{
    [SerializeField]
    private BossConductor BossConductor;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            if (collision.TryGetComponent(out IDamageable damageable))
            {
                damageable.OnOneShot();
            }

            BossConductor.RestartBattle();
        }
    }
}
