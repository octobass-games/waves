using Octobass.Waves;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    public MainMenu MainMenu;
    public List<Animator> Buttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadButton(int button)
    {
        Buttons[button].SetTrigger("load");
    }

    public void EndAnimation()
    {
        MainMenu.EnableButtons();
    }
}
