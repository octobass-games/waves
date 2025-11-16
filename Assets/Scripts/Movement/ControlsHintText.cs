using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsHintText : MonoBehaviour
{
    public TextMeshPro Text;
    public InputActionReference ActionReference;

    private string GetFirstNonCompositeBindingDisplay(InputAction action)
    {
        var bindingString = "";

        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];

            // Skip composite itself
            if (binding.isComposite) continue;


            if (binding.isPartOfComposite)
            {
                bindingString = bindingString + " " +  InputControlPath.ToHumanReadableString(binding.effectivePath);
                continue;
            }
           
            return InputControlPath.ToHumanReadableString(binding.effectivePath);
        }

        return bindingString;
    }


    void Start()
    {
        InputAction action = ActionReference.action;

       string str = InputControlPath.ToHumanReadableString(GetFirstNonCompositeBindingDisplay(action));

        Text.text = str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
