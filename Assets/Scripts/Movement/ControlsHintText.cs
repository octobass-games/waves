using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsHintText : MonoBehaviour
{
    public TextMeshPro Text;
    public InputActionReference ActionReference;

    [SerializeField]
    private UnityEngine.InputSystem.PlayerInput PlayerInput;

    [SerializeField]
    private List<string> CompositeParts;

    public ControlsImageMap imageMap;
    public SpriteRenderer ControlSprite;

    void Update()
    {
        string result = "";

        if (CompositeParts.Count > 0)
        {
            foreach (var part in CompositeParts)
            {
                result += ActionReference.action.GetBindingDisplayString(ActionReference.action.GetBindingIndex(bindingMask: new InputBinding { name = part, groups = PlayerInput.currentControlScheme }));
            }
        }
        else
        {
            result = ActionReference.action.GetBindingDisplayString(ActionReference.action.GetBindingIndex(bindingMask: new InputBinding { groups = PlayerInput.currentControlScheme }));
        }

        var matchingImage = imageMap.Images.Find(i => i.MatchingString.ToLower() == result.ToLower());

        Debug.Log("Controls: " + result.ToLower());

        if (matchingImage != null)
        {
            Text.gameObject.SetActive(false);
            ControlSprite.gameObject.SetActive(true);
            ControlSprite.sprite = matchingImage.Sprite;
        }
        else
        {
            Text.text = result;
            Text.gameObject.SetActive(true);
            ControlSprite.gameObject.SetActive(false);
        }
    }
}
