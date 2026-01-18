using Octobass.Waves.Map;
using UnityEngine;

public class FinalShell : MonoBehaviour
{
    public Cartographer Cartographer;
    public Dialogue dialogue;
    
    public void Interact()
    {
       if (Cartographer.GetFoundShellCount() == 48)
        {
            Debug.Log("Secret end animation");
        }else
        {
            dialogue.RenderDialog();
        }
    }
}
