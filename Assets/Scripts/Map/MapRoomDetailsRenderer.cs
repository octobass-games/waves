using UnityEngine;
using UnityEngine.UI;

public class MapRoomDetailsRenderer : MonoBehaviour
{
    public GameObject Player;
    public GameObject Waterporter;
    public GameObject Shell;
    public GameObject SelectedBorder;

    public void ResetAllOpacities()
    {
        Player.GetComponent<Image>().color = Color.white;
        SelectedBorder.GetComponent<Image>().color = Color.white;
        Waterporter.GetComponent<Image>().color = Color.white;
        Shell.GetComponent<Image>().color = Color.white;
    }
}
