using Octobass.Waves.Item;
using Octobass.Waves.Map;
using Octobass.Waves.Movement;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Octobass.Waves.Ability
{
    public class AbilityController : MonoBehaviour
    {
        public UnityEvent OnUpgradeStart;
        public UnityEvent OnUpgradeEnd;

        [SerializeField]
        private MovementController MovementController;

        [SerializeField]
        private AnimationController AnimationController;

        [SerializeField]
        private GameObject ExplainerRoot;
        
        [SerializeField]
        private TextMeshProUGUI ExplainerText;
        
        private AbilityItemInstance PickedUpItem;

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
                PickedUpItem = abilityItemInstance;

                MovementController.AddState(PickedUpItem.Ability.NewState);
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
            ExplainerText.text = PickedUpItem.Ability.Explainer;
        }

        public void OnUpgradeExplainerDismissed()
        {
            PickedUpItem = null;
            ExplainerRoot.SetActive(false);
            ExplainerText.text = "";
            OnUpgradeEnd.Invoke();
        }
    }
}
