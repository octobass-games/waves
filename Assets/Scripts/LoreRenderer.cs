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

    void Awake()
    {
        initaliseRenderers(null);
        EventSystem.current.SetSelectedGameObject(BackButton.gameObject);
    }

    public void PickItem(ItemDefinition item)
    {
        var instance = inventory.FindItem(item);
        inspector.gameObject.SetActive(true);
        inspector.OnItemPickedUp(instance);

        initaliseRenderers(item);
    }

    private void initaliseRenderers(ItemDefinition active)
    {
        renderers.ForEach(renderer =>
        {
            var item = inventory.FindItem(renderer.ItemDefinition);
            renderer.gameObject.SetActive(item != null && renderer.ItemDefinition != active);
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
