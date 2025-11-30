using Octobass.Waves.Item;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LoreRenderer : MonoBehaviour
{
    public LoreInspector inspector;
    public Inventory inventory;

    public List<LoreItemRenderer> renderers;
    public Button BackButton;
    public Button CloseInspectButtonNonFlippable;

    void OnEnable()
    {
        initaliseRenderers(null);
        EventSystem.current.SetSelectedGameObject(BackButton.gameObject);
        inspector.gameObject.SetActive(false);
    }

    public void PickItem(ItemDefinition item)
    {
        var instance = inventory.FindItem(item);
        inspector.gameObject.SetActive(true);
        inspector.OnItemPickedUp(instance);
        hideRenderes();

        if (instance is LoreItemInstance loreItemInstance)
        {
            if (loreItemInstance.Definition.BackSprite == null)
            {
                EventSystem.current.SetSelectedGameObject(CloseInspectButtonNonFlippable.gameObject);
            }
        }
    }

    public void CloseInspect()
    {
        inspector.gameObject.SetActive(false);
        initaliseRenderers(null);
        EventSystem.current.SetSelectedGameObject(BackButton.gameObject);
    }

    private void hideRenderes()
    {
        renderers.ForEach(renderer =>
        {
            renderer.gameObject.SetActive(false);
        });
    }

    private void initaliseRenderers(ItemDefinition active)
    {
        renderers.ForEach(renderer =>
        {
            var item = inventory.FindItem(renderer.ItemDefinition);
            renderer.gameObject.SetActive(item != null && renderer.ItemDefinition != active);
            if (active != null) {
                if (item is LoreItemInstance loreItemInstance)
                {
                    if (loreItemInstance.Definition.BackSprite == null)
                    {
                        EventSystem.current.SetSelectedGameObject(BackButton.gameObject);
                    }
                }
            }
        });
    }

    void Update()
    {
           if (EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(BackButton.gameObject);
            }
    }
}
