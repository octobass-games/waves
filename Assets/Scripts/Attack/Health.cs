using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Attack
{
    public class Health : MonoBehaviour, IDamageable
    {
        public int HealthPoints;
        public int MaxHealthPoints = 1;
        public UnityEvent OnDamageTaken;
        public UnityEvent OnHealthEmpty;

        private bool HasRespawned;

        void Awake()
        {
            if (HealthPoints <= 0)
            {
                Debug.LogWarning($"[Health]: HealthPoints not set to positive integer for {name}");
            }
        }

        public void OnHit()
        {
            HealthPoints -= 1;

            if (HealthPoints == 0)
            {
                OnHealthEmpty.Invoke();
            }
            else
            {
                OnDamageTaken.Invoke();
            }
        }

        public void OnOneShot()
        {
            if (!HasRespawned)
            {
                HasRespawned = true;

                HealthPoints = 0;

                OnHealthEmpty.Invoke();
            }
        }

        public void Reset()
        {
            HealthPoints = MaxHealthPoints;
            HasRespawned = false;
        }
    }
}
