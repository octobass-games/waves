using Octobass.Waves.Movement;
using TMPro;
using UnityEngine;

namespace Octobass.Waves.Item
{
    public class LoreInspector : MonoBehaviour
    {
        [SerializeField]
        private GameObject LoreRoot;

        [SerializeField]
        private TextMeshProUGUI LoreText;

        [SerializeField]
        private MovementController MovementController;

        void Awake()
        {
            if (LoreRoot == null)
            {
                Debug.LogWarning("[LoreInspector]: LoreRoot not set");
            }

            if (LoreText == null)
            {
                Debug.LogWarning("[LoreInspector]: LoreText not set");
            }

            if (MovementController == null)
            {
                Debug.LogWarning("[LoreInspector]: MovementController not set");
            }
        }

        public void OnItemPickedUp(ItemInstance item)
        {
            if (item is LoreItemInstance loreItemInstance)
            {
                LoreRoot.SetActive(true);
                LoreText.text = loreItemInstance.GetText();
                
                MovementController.Freeze();
            }
        }

        public void OnLoreInspected()
        {
            LoreRoot.SetActive(false);
            
            MovementController.Unfreeze();
        }
    }
}
