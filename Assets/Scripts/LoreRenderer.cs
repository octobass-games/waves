using Octobass.Waves.Item;
using System.Collections.Generic;
using UnityEngine;

public class LoreRenderer : MonoBehaviour
{
    public LoreInspector inspector;
    public Inventory inventory;

    public List<LoreItemRenderer> renderers;

    void Awake()
    {
        renderers.ForEach(renderer =>
        {
            var item = inventory.FindItem(renderer.ItemDefinition);
            renderer.gameObject.SetActive(item != null);
        });
    }

    public void PickItem(ItemDefinition item)
    {
        var instance = inventory.FindItem(item);
        inspector.gameObject.SetActive(true);
        inspector.OnItemPickedUp(instance);
    }
}
