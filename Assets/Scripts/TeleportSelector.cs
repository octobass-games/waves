using Octobass.Waves.Spawn;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportSelector : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
{
    [SerializeField]
    private SpawnPoint SpawnPoint;

    [SerializeField]
    private TeleportController TeleportController;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
        TeleportController.BeginTeleport();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        TeleportController.SetDestination(SpawnPoint);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Enter controller!");
        TeleportController.SetDestination(SpawnPoint);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log("Submit!");
        TeleportController.Teleport();
    }
}
