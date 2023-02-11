using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [HideInInspector]
    public Text MoneyCount;
    [HideInInspector]
    public Text CrystalCount;

    private void Start() 
    {
        MoneyCount = transform.Find("MoneyCount").GetComponent<Text>();
        CrystalCount = transform.Find("CrystalCount").GetComponent<Text>();
    }

    private void Update() 
    {
        MoneyCount.text = GameModel.MoneyCount.ToString();
        CrystalCount.text = GameModel.CrystalCount.ToString();
    }
}
