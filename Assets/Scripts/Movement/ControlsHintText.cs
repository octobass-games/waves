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

        Text.text = result;
    }
}
