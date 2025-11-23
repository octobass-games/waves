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
        TeleportController.BeginTeleport();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TeleportController.SetDestination(SpawnPoint);
    }

    public void OnSelect(BaseEventData eventData)
    {
        TeleportController.SetDestination(SpawnPoint);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        TeleportController.BeginTeleport();
    }
}
