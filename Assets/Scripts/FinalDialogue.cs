using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class FinalDialogue : MonoBehaviour
{
    [SerializeField]
    private BossConductor BossConductor;

    void OnEnable()
    {
        InputSystem.onAnyButtonPress.CallOnce((_) => BossConductor.StartBattle());
    }
}
