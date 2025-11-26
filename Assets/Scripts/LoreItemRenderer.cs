using Octobass.Waves.Item;
using UnityEngine;
using UnityEngine.UI;

public class LoreItemRenderer : MonoBehaviour
{
    public ItemDefinition ItemDefinition;
    public Button Button;
    public LoreRenderer Renderer;

    private void Awake()
    {
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => Renderer.PickItem(ItemDefinition));
    }
}
