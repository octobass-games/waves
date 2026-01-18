using System.Linq;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [TextArea]
    public string Text;

    public DialogueRenderer dialogueRenderer;

    public void RenderDialog()
    {
        dialogueRenderer.RenderDialgoue(Text.Split('\n').ToList());
    }
}
