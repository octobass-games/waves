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

    private string StringifiedBinding;
    private string ReadableStringifiedBinding;

    void Start()
    {
        ReadableStringifiedBinding = "";

        if (CompositeParts.Count > 0)
        {
            foreach (var part in CompositeParts)
            {
                ReadableStringifiedBinding += ActionReference.action.GetBindingDisplayString(ActionReference.action.GetBindingIndex(bindingMask: new InputBinding { name = part, groups = PlayerInput.currentControlScheme }));
            }
        }
        else
        {
            ReadableStringifiedBinding = ActionReference.action.GetBindingDisplayString(ActionReference.action.GetBindingIndex(bindingMask: new InputBinding { groups = PlayerInput.currentControlScheme }));
        }

        StringifiedBinding = Regex.Replace(ReadableStringifiedBinding.ToLower(), @"\s+", "");
        
        Debug.Log("Controls: " + StringifiedBinding);
    }

    void Update()
    {
        var matchingImage = imageMap.Images.Find(i => i.MatchingString.ToLower() == StringifiedBinding);


        if (matchingImage != null)
        {
            Text.gameObject.SetActive(false);
            ControlSprite.gameObject.SetActive(true);
            ControlSprite.sprite = matchingImage.Sprite;
        }
        else
        {
            Text.text = ReadableStringifiedBinding;
            Text.gameObject.SetActive(true);
            ControlSprite.gameObject.SetActive(false);
        }
    }
}
