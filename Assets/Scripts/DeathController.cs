using Octobass.Waves.Movement;
using Octobass.Waves.Spawn;
using UnityEngine;

namespace Octobass.Waves
{
    public class DeathController : MonoBehaviour
    {
        [SerializeField]
        private AnimationController AnimationController;

        [SerializeField]
        private MovementController MovementController;

        [SerializeField]
        private SpawnTracker SpawnTracker;

        void Awake()
        {
            if (AnimationController == null)
            {
                Debug.LogWarning("[DeathController]: AnimationController not set");
            }

            if (MovementController == null)
            {
                Debug.LogWarning("[DeathController]: MovementController not set");
            }

            if (SpawnTracker == null)
            {
                Debug.LogWarning("[DeathController]: SpawnTracker not set");
            }
        }

        public void Die()
        {
            MovementController.Freeze();
            AnimationController.PlayDeathAnimation();
        }

        void OnDeathAnimationEnd()
        {
            SpawnTracker.Respawn();
        }

        void OnRespawnAnimationEnd()
        {
            MovementController.Unfreeze();
        }
    }
}
