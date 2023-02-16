using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidResumeButton : MonoBehaviour
{
    public GameObject button;
    public static int UseCount;
    private void Update() 
    {
        if (UseCount < 1 && GameModel.MoneyCount >= 300)
            button.SetActive(true);
        else if (UseCount > 0 || GameModel.MoneyCount < 300)
            button.SetActive(false);
    }

    public void PayForResume()
    {
        GameModel.MoneyCount -= 300;
        UseCount += 1;
        GameModel.SaveToFile();
    }
}
