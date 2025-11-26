using Octobass.Waves;
using Octobass.Waves.Map;
using UnityEngine;
using UnityEngine.EventSystems;

public class TeleportSelector : MonoBehaviour, IPointerClickHandler, ISubmitHandler
{
    private RoomId TeleporterRoom;
    private TeleportController TeleportController;

    void Start()
    {
        TeleportController = FindFirstObjectByType<TeleportController>();

        if (TeleportController == null)
        {
            Debug.Log("[TeleportSelector]: TeleportController not found");
        }

        if (TryGetComponent(out MapRoomRenderer mapRoomRenderer))
        {
            TeleporterRoom = mapRoomRenderer.Id;
        }
        else
        {
            Debug.Log("[TeleportSelector]: MapRoomRenderer not found");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Teleport();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Teleport();
    }

    private void Teleport()
    {
        TeleportController.Teleport(TeleporterRoom);
    }
}
