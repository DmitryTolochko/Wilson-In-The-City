using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    private Image Image;
    private Text Price;
    private GameObject Crystal;
    
    public Sprite ClosedSprite;
    public Sprite OpenedSprite;
    public Sprite ChosenSprite;

    public SkinType SkinType;

    public int Cost;

    private void Start() 
    {
        Image = GetComponent<Image>();
        Price = transform.Find("Price").GetComponent<Text>();
        Price.text = Cost == 0 ? "" : Cost.ToString();
        Crystal = transform.Find("Image").gameObject;
    }

    public void ButtonClick()
    {
        if (GameModel.OpenedCharacters.Contains(SkinType) && GameModel.SkinType == SkinType)
            return;
        
        if (GameModel.OpenedCharacters.Contains(SkinType) && GameModel.SkinType != SkinType)
        {
            PlayerAnimator.ChangeSkin(SkinType);
        }
        else
        {
            BuyCharacter();
        }
    }

    public void UpdateCard()
    {
        if (GameModel.OpenedCharacters.Contains(SkinType) && GameModel.SkinType != SkinType)
        {
            Image.sprite = OpenedSprite;
            Price.text = "";
            Crystal.SetActive(false);
        }
        else if (GameModel.OpenedCharacters.Contains(SkinType) && GameModel.SkinType == SkinType)
        {
            Image.sprite = ChosenSprite;
            Price.text = "";
            Crystal.SetActive(false);
        }
        else
            Image.sprite = ClosedSprite;
    }

    private void BuyCharacter()
    {
        GameModel.CrystalCount -= Cost;
        GameModel.OpenedCharacters.Add(SkinType);
        GameModel.SaveToFile();
    }
}
