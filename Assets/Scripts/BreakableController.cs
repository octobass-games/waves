using Octobass.Waves.Save;
using UnityEngine;

namespace Octobass.Waves
{
    public class BreakableController : MonoBehaviour, ISavable
    {
        [SerializeField]
        private string BreakableId;

        [SerializeField]
        private Animator Animator;

        [SerializeField]
        private BoxCollider2D BreakableCollider;

        [SerializeField]
        private BoxCollider2D BreakableRangeCollider;

        private bool IsBroken;

        private string SaveKey;

        void Awake()
        {
            if (BreakableId == "")
            {
                Debug.Log($"[BreakableController]: BreakableId not set for {gameObject.name}");
            }

            if (Animator == null)
            {
                Debug.Log($"[BreakableController]: Animator not set for {gameObject.name}");
            }

            if (BreakableCollider == null)
            {
                Debug.Log($"[BreakableController]: BreakableCollider not set for {gameObject.name}");
            }

            if (BreakableRangeCollider == null)
            {
                Debug.Log($"[BreakableController]: BreakableRangeCollider not set for {gameObject.name}");
            }

            SaveKey = $"breakable-{BreakableId}-broken";
        }

        public void Break()
        {
            IsBroken = true;

            Animator.SetTrigger("break");

            DisableColliders();
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(SaveKey, IsBroken);
        }

        public void Load(SaveData saveData)
        {
            IsBroken = saveData.Load<bool>(SaveKey);

            if (IsBroken)
            {
                Animator.SetBool("ImmediateBreak", true);

                DisableColliders();
            }
        }

        private void DisableColliders()
        {
            BreakableCollider.enabled = false;
            BreakableRangeCollider.enabled = false;
        }
    }
}
