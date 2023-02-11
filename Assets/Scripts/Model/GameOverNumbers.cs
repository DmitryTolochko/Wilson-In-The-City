using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverNumbers : MonoBehaviour
{
    [HideInInspector]
    public Text BestScore, LastScore, MoneyCount, CrystalCount;

    public static GameOverNumbers Instance;

    private void Start() 
    {
        BestScore = transform.Find("BestScore").GetComponent<Text>();
        LastScore = transform.Find("LastScore").GetComponent<Text>();
        MoneyCount = transform.Find("Money").GetComponent<Text>();
        CrystalCount = transform.Find("Crystals").GetComponent<Text>();

        Instance = this;
    }

    public static void Refresh()
    {
        Instance.BestScore.text = ((int)GameModel.BestScore).ToString();
        Instance.LastScore.text = ((int)GameModel.ScoreCurrent).ToString();
        Instance.CrystalCount.text = GameModel.CrystalCount.ToString();
        Instance.MoneyCount.text = GameModel.MoneyCount.ToString();
    }
}
