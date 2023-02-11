using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public List<GameObject> Buttons = new List<GameObject>();

    private List<CharacterButton> ButtonsScripts = new List<CharacterButton>();

    private void Start() 
    {
        foreach (var button in Buttons)
        {
            ButtonsScripts.Add(button.GetComponent<CharacterButton>());
        }
    }

    private void Update() 
    {
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        foreach (var button in ButtonsScripts)
        {
            button.UpdateCard();
        }
    }
}
