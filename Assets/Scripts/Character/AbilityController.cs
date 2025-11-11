using Octobass.Waves.Item;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Character
{
    public class AbilityController : MonoBehaviour
    {
        public MovementController MovementController;
        public AnimationController AnimationController;

        public GameObject ExplainerRoot;
        public TextMeshProUGUI ExplainerText;

        public UnityEvent OnUpgradeStart;
        public UnityEvent OnUpgradeEnd;

        void Awake()
        {
            if (MovementController == null)
            {
                Debug.LogWarning("[AbilityController]: MovementController not set");
            }

            if (AnimationController == null)
            {
                Debug.LogWarning("[AbilityController]: AnimationController not set");
            }

            if (ExplainerRoot == null)
            {
                Debug.LogWarning("[AbilityController]: ExplainerRoot not set");
            }

            if (ExplainerText == null)
            {
                Debug.LogWarning("[AbilityController]: ExplainerText not set");
            }
        }

        public void OnItemPickedUp(ItemInstance item)
        {
            if (item is AbilityItemInstance abilityItemInstance)
            {
                MovementController.AddState(abilityItemInstance.NewState);
                AnimationController.PlayUpgradeAnimation();
            }
        }

        public void OnUpgradeAnimationStart()
        {
            OnUpgradeStart.Invoke();
        }

        public void OnUpgradeAnimationEnd()
        {
            ExplainerRoot.SetActive(true);
            ExplainerText.text = "Hello, World!";
        }

        public void OnUpgradeExplainerDismissed()
        {
            ExplainerRoot.SetActive(false);
            ExplainerText.text = "";
            OnUpgradeEnd.Invoke();
        }
    }
}
