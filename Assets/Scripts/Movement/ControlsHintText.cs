using System.Collections.Generic;
using System.Text.RegularExpressions;
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
        string readableStringifiedBinding = "";

        if (CompositeParts.Count > 0)
        {
            foreach (var part in CompositeParts)
            {
                readableStringifiedBinding += ActionReference.action.GetBindingDisplayString(ActionReference.action.GetBindingIndex(bindingMask: new InputBinding { name = part, groups = PlayerInput.currentControlScheme }));
            }
        }
        else
        {
            readableStringifiedBinding = ActionReference.action.GetBindingDisplayString(ActionReference.action.GetBindingIndex(bindingMask: new InputBinding { groups = PlayerInput.currentControlScheme }));
        }

        string stringifiedBinding = Regex.Replace(readableStringifiedBinding.ToLower(), @"\s+", "");
        
        var matchingImage = imageMap.Images.Find(i => i.MatchingString.ToLower() == stringifiedBinding);

        if (matchingImage != null)
        {
            Text.gameObject.SetActive(false);
            ControlSprite.gameObject.SetActive(true);
            ControlSprite.sprite = matchingImage.Sprite;
        }
        else
        {
            Text.text = readableStringifiedBinding;
            Text.gameObject.SetActive(true);
            ControlSprite.gameObject.SetActive(false);
        }
    }
}
