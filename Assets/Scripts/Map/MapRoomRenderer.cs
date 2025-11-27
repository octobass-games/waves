using UnityEngine;
using UnityEngine.UI;

namespace Octobass.Waves.Map
{
    public class MapRoomRenderer : MonoBehaviour
    {
        public RoomId Id;

        private Image Image;
        private Button Button;
        private TeleportSelector TeleportSelector;
        private float Opacity;

        private MapRoomDetailsRenderer detailsRenderer;

        public void Draw(Room room, bool isPlayerInRoom, bool miniMode, bool teleportMode)
        {
            Image = GetComponent<Image>();
            Button = GetComponent<Button>();
            TeleportSelector = GetComponent<TeleportSelector>();

            detailsRenderer = GetComponentInChildren<MapRoomDetailsRenderer>();

            Color color = Image.color;

            if (room.State == RoomState.Unknown)
            {
                Image.enabled = false;
            }
            else
            {
                Image.enabled = true;
                Image.color = new Color(color.r, color.g, color.b, room.State == RoomState.Discovered ? 0.5f : 1f);
            }
            if (!miniMode)
            {
                detailsRenderer.Player.SetActive(isPlayerInRoom);
            }
            if (room.IsShellFound)
            {
                detailsRenderer.Shell.SetActive(true);
            }

            detailsRenderer.Waterporter.SetActive(room.IsTeleporterFound);

            if (teleportMode)
            {
                Button.GetComponent<Animator>().enabled = true;
                Button.enabled = true;
                Button.interactable = room.IsTeleporterFound;

                if (room.IsTeleporterFound)
                {
                    if (TeleportSelector != null)
                    {
                        TeleportSelector.enabled = true;
                    }
                }
            }
            else
            {
                Button.enabled = false;
                Button.GetComponent<Animator>().enabled = false;
                if (TeleportSelector != null)
                {
                    TeleportSelector.enabled = false;
                }
                detailsRenderer.SelectedBorder.SetActive(false);
                detailsRenderer.ResetAllOpacities();
            }
        }
    }
}
