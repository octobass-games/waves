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
        private Inspectable Inspectable;

        private bool IsUnlocked;

        private string SaveKey = "grate-unlocked";

        public void Load(SaveData saveData)
        {
            IsUnlocked = saveData.Load<bool>(SaveKey);

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
            GrateAnimator.SetBool("Closed", false);
            LeverAnimator.SetBool("Left", false);

            GrateCollider.enabled = false;
            Inspectable.enabled = false;
        }
    }
}
