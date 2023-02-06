using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [HideInInspector]
    public Text Odometer;
    [HideInInspector]
    public Text MoneyCount;
    [HideInInspector]
    public Text CrystalCount;

    private void Start() 
    {
        Odometer = transform.Find("Odometer").GetComponent<Text>();
        MoneyCount = transform.Find("MoneyCount").GetComponent<Text>();
        CrystalCount = transform.Find("CrystalCount").GetComponent<Text>();
    }

    private void Update() 
    {
        Odometer.text = ((int)GameModel.ScoreCurrent).ToString() + " meters";
        MoneyCount.text = GameModel.MoneyCountCurrent.ToString();
        CrystalCount.text = GameModel.CrystalsCountCurrent.ToString();
    }
}
