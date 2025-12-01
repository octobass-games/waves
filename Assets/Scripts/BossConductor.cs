using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BossConductor : MonoBehaviour
{
    public CinemachineCamera Camera;
    public Transform Boss;
    public Button Button;
    public GameObject DialoguePanel;
    private InputActionMap UiActionMap;
    public PlayerInput PlayerInput;
    private InputActionMap GameplayInput;
    public Animator BossAnimator;

    [SerializeField]
    private CinemachineCamera BossCamera;

    [SerializeField]
    private CinemachineCamera OpeningCamera;


    void Start()
    {
        GameplayInput = PlayerInput.actions.FindActionMap("Gameplay");
        UiActionMap = PlayerInput.actions.FindActionMap("UI");
    }
    public void StartBattle()
    {
        Debug.Log("Boss: StartBattle");
        DialoguePanel.SetActive(false);
        PlayerInput.SwitchCurrentActionMap(GameplayInput.name);
        BossAnimator.SetTrigger("StartBattle");
        Camera.gameObject.SetActive(false);
        OpeningCamera.gameObject.SetActive(false);
        BossCamera.gameObject.SetActive(true);
    }

    public void RestartBattle()
    {
        BossAnimator.SetTrigger("StartBattle");
    }

    public void OpenDialogue()
    {
        Debug.Log("Boss: OpenDialogue");
        DialoguePanel.SetActive(true);
        PlayerInput.SwitchCurrentActionMap(UiActionMap.name);
        EventSystem.current.SetSelectedGameObject(Button.gameObject);
    }

    public void FinishBattle()
    {
        Debug.Log("Finished");
    }
}
