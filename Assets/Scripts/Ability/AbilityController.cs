using Octobass.Waves.Item;
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
        private AbilityExplainer AbilityExplainer;
        
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

            if (AbilityExplainer == null)
            {
                Debug.LogWarning("[AbilityController]: AbilityExplainer not set");
            }
        }

        public void OnItemPickedUp(ItemInstance item)
        {
            if (item is AbilityItemInstance abilityItemInstance)
            {
                PickedUpItem = abilityItemInstance;

                MovementController.AddState(PickedUpItem.Ability.NewState);
                MovementController.Freeze();
                AnimationController.PlayUpgradeAnimation();
            }
        }

        public void OnUpgradeAnimationStart()
        {
            OnUpgradeStart.Invoke();
        }

        public void OnUpgradeAnimationEnd()
        {
            AbilityExplainer.Explain(PickedUpItem.Ability);
        }

        public void EndUpgrade()
        {
            PickedUpItem = null;
            MovementController.Unfreeze();
            OnUpgradeEnd.Invoke();
        }
    }
}
