using Octobass.Waves.Spawn;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportSelector : MonoBehaviour, IPointerClickHandler, ISubmitHandler
{
    [SerializeField]
    private SpawnPoint SpawnPoint;

    [SerializeField]
    private TeleportController TeleportController;

    public void OnPointerClick(PointerEventData eventData)
    {
        TeleportController.SetDestination(SpawnPoint);
        TeleportController.Teleport();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        TeleportController.SetDestination(SpawnPoint);
        TeleportController.Teleport();
    }
}
