using Octobass.Waves.Movement;
using Octobass.Waves.Spawn;
using UnityEngine;
using UnityEngine.InputSystem;

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

        [SerializeField]
        private PlayerInput PlayerInput;

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
            PlayerInput.actions.FindActionMap("Gameplay").Disable();
            PlayerInput.actions.FindActionMap("Global").Disable();
            MovementController.Freeze();
            AnimationController.PlayDeathAnimation();
        }

        public void OnDeathAnimationEnd()
        {
            SpawnTracker.Respawn();
        }

        public void OnRespawnAnimationEnd()
        {
            PlayerInput.actions.FindActionMap("Gameplay").Enable();
            PlayerInput.actions.FindActionMap("Global").Enable();
            MovementController.Unfreeze();
        }
    }
}
