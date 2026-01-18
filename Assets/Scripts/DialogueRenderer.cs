using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogueRenderer : MonoBehaviour
{

    private int pos = 0;
    private List<string> Text;
    public TextMeshProUGUI TextGUI;
    public GameObject DialogueGameObject;
    public GameObject NextButton;
    public PlayerInput PlayerInput;

   public void RenderDialgoue(List<string> text)
    {
        pos = 0;
        Text = text;

        TextGUI.text = Text[pos];
        DialogueGameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(NextButton);
        PlayerInput.SwitchCurrentActionMap("UI");

    }

    public void Next()
    {
        pos ++;
        if (pos == Text.Count)
        {
            DialogueGameObject.SetActive(false);
            PlayerInput.SwitchCurrentActionMap("Gameplay");
        }
        else
        {
            TextGUI.text = Text[pos];
        }

    }
}
