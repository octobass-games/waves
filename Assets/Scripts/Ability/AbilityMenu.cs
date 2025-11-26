using Octobass.Waves.Item;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Octobass.Waves.Ability
{
    public class AbilityMenu : MonoBehaviour
    {
        [SerializeField]
        private List<AbilityMenuItem> AbilityMenuItems;

        [SerializeField]
        private TextMeshProUGUI Text;

        [SerializeField]
        private Inventory Inventory;

        [SerializeField]
        private GameObject BackButton;

        [SerializeField]
        private GameObject CloseButton;

        private GameObject MostRecentlySelectedGameObject;

        void OnEnable()
        {
            List<AbilityItemInstance> unlockedAbilityItems = Inventory.GetAbilityItems();

            foreach (AbilityMenuItem abilityMenuItem in AbilityMenuItems)
            {
                if (unlockedAbilityItems.Exists(abilityItem => abilityItem.Ability.Name == abilityMenuItem.AbilityDefinition.Name))
                {
                    abilityMenuItem.gameObject.SetActive(true);
                    abilityMenuItem.RegisterOnSelect(UpdateExplainer);
                }
                else
                {
                    abilityMenuItem.gameObject.SetActive(false);
                }
            }

            if (unlockedAbilityItems.Count > 0)
            {
                MostRecentlySelectedGameObject = AbilityMenuItems.Find(menuItem => menuItem.isActiveAndEnabled).gameObject;
            }
            else
            {
                MostRecentlySelectedGameObject = BackButton;
            }

            EventSystem.current.SetSelectedGameObject(MostRecentlySelectedGameObject);
        }

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject != MostRecentlySelectedGameObject)
            {
                MostRecentlySelectedGameObject = EventSystem.current.currentSelectedGameObject;
                Debug.Log($"Newly selected game object: {MostRecentlySelectedGameObject.name}");
            }
            else if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(MostRecentlySelectedGameObject);
            }
        }

        private void UpdateExplainer(AbilityDefinition abilityDefinition)
        {
            Text.text = abilityDefinition.Name;
        }
    }
}
