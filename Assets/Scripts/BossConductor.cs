using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.UI;
using static UnityEngine.Timeline.DirectorControlPlayable;

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
    private CinemachineFollow Follower;

    [SerializeField]
    private float BossXOffset;


    void Start()
    {
        GameplayInput = PlayerInput.actions.FindActionMap("Gameplay");
        UiActionMap = PlayerInput.actions.FindActionMap("UI");
    }
    public void StartBattle()
    {
        Debug.Log("Boss: StartBattle");
        Camera.Follow = Boss;
        DialoguePanel.SetActive(false);
        PlayerInput.SwitchCurrentActionMap(GameplayInput.name);
        BossAnimator.SetTrigger("StartBattle");
        Follower.FollowOffset.x = BossXOffset;
    }

    public void OpenDialogue()
    {
        Debug.Log("Boss: OpenDialogue");
        DialoguePanel.SetActive(true);
        PlayerInput.SwitchCurrentActionMap(UiActionMap.name);
        EventSystem.current.SetSelectedGameObject(Button.gameObject);
    }
}
