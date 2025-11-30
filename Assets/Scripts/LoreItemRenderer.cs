using Octobass.Waves.Item;
using UnityEngine;
using UnityEngine.UI;

public class LoreItemRenderer : MonoBehaviour
{
    public LoreItemDefinition ItemDefinition;
    public Button Button;
    public LoreRenderer Renderer;
    public Image Image;

    void Awake()
    {
        Image.sprite = ItemDefinition.FrontSprite;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener(() => Renderer.PickItem(ItemDefinition));
    }
}
