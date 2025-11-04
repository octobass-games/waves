using System.Collections.Generic;
using UnityEngine;

namespace Octobass.Waves.Map
{
    public class MapRenderer : MonoBehaviour
    {
        public Cartographer Cartographer;
        public List<MapRoomRenderer> RoomRenderers;

        public bool miniMode = true;

        public GameObject MiniMap;
        public GameObject BigMap;

        public void ToggleMode()
        {
            miniMode = !miniMode;

            if (miniMode)
            {
                MiniMap.SetActive(true);
                BigMap.SetActive(false);
            }
            else
            {
                MiniMap.SetActive(false);
                BigMap.SetActive(true);
            }
        }
    }
}
