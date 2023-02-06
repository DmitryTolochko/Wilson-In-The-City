using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [HideInInspector]
    public Text Odometer;
    [HideInInspector]
    public Text MoneyCount;
    [HideInInspector]
    public Text CrystalCount;

    private GameObject ClockButton;
    private GameObject ClockTimer;

    private void Start() 
    {
        Instance = this; 

        Odometer = transform.Find("Odometer").GetComponent<Text>();
        MoneyCount = transform.Find("MoneyCount").GetComponent<Text>();
        CrystalCount = transform.Find("CrystalCount").GetComponent<Text>();

        ClockButton = transform.Find("ClockButton").gameObject;
        ClockTimer = transform.Find("ClockTimer").gameObject;

        GameModel.IncreaseClockNum += () => ChangeClockCount();
    }

    private void Update() 
    {
        Odometer.text = ((int)GameModel.ScoreCurrent).ToString() + " meters";
        MoneyCount.text = GameModel.MoneyCountCurrent.ToString();
        CrystalCount.text = GameModel.CrystalsCountCurrent.ToString();

        if (GameModel.CollectedClocksCurrent > 0)
            ClockButton.SetActive(true);
        else if (GameModel.CollectedClocksCurrent == 0 && ClockTimer.activeSelf)
            return;
        else
            ClockButton.SetActive(false);
    }

    private void ChangeClockCount()
    {
        if (GameModel.CollectedClocksCurrent <= 1)
            ClockButton.transform.Find("Text").GetComponent<Text>().text = "";
        if (GameModel.CollectedClocksCurrent > 1)
            ClockButton.transform.Find("Text").GetComponent<Text>().text = 
                "x" + GameModel.CollectedClocksCurrent.ToString();
    }

    public static IEnumerator StartClockTimer()
    {
        Instance.ClockTimer.SetActive(true);
        var image = Instance.ClockTimer.GetComponent<Image>();
        image.fillAmount = 1;
        var a = 1.0f;
        for (var i = 1; i <= 15; i++)
        {
            a -= 1/15f;
            print(a);
            image.fillAmount = a;
            yield return new WaitForSeconds(1);
        }
        Instance.ClockTimer.SetActive(false);
    }

    public static void StopClockTimer()
    {
        Instance.ClockTimer.SetActive(false);
    }
}
