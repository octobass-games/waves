using Octobass.Waves.Item;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Octobass.Waves.Ability
{
    public class AbilityMenu : MonoBehaviour
    {
        [SerializeField]
        private List<AbilityMenuItem> AbilityMenuItems;

        [SerializeField]
        private Image Image;

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
                    abilityMenuItem.RegisterOnSelect(ShowExplainer);
                    abilityMenuItem.RegisterOnDeselect(HideExplainer);
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

        private void ShowExplainer(AbilityDefinition abilityDefinition)
        {
            Image.enabled = true;
            Image.sprite = abilityDefinition.Image;
            Image.SetNativeSize();
        }

        private void HideExplainer()
        {
            Image.enabled = false;
        }
    }
}
