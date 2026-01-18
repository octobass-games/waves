using Octobass.Waves.Item;
using Octobass.Waves.Save;
using UnityEngine;

namespace Octobass.Waves
{
    public class GrateController : MonoBehaviour, ISavable
    {
        [SerializeField]
        private Animator GrateAnimator;

        [SerializeField]
        private Animator LeverAnimator;

        [SerializeField]
        private BoxCollider2D GrateCollider;

        [SerializeField]
        private BoxCollider2D LeverCollider;

        [SerializeField]
        private Inspectable Inspectable;

        private bool IsUnlocked;

        public string SaveKey = "grate-unlocked";

        public void Load(SaveData saveData)
        {
            IsUnlocked = saveData.Load<bool>(SaveKey);
            Debug.Log("load" + SaveKey + IsUnlocked);

            if (IsUnlocked)
            {
                Open();
            }
        }

        public void Save(SaveData saveData)
        {
            saveData.Add(SaveKey, IsUnlocked);
        }

        public void Unlock()
        {
            if (!IsUnlocked)
            {
                IsUnlocked = true;
                
                Open();
            }
        }

        private void Open()
        {
            if (GrateAnimator != null)
            {
            GrateAnimator.SetBool("Closed", false);

            }
            if (LeverAnimator != null)
            {
                LeverAnimator.SetBool("Left", false);
            }


            GrateCollider.enabled = false;

            if (LeverCollider != null)
            {
                LeverCollider.enabled = false;
            }

            if (Inspectable != null)
            {
                Inspectable.enabled = false;
            }
        }
    }
}
