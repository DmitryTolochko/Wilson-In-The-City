using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidResumeButton : MonoBehaviour
{
    private void Update() 
    {
        if (GameModel.MoneyCount >= 300 && !gameObject.activeSelf)
            gameObject.SetActive(true);
        else if (GameModel.MoneyCount < 300 && gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public void PayForResume()
    {
        GameModel.MoneyCount -= 300;
        GameModel.SaveToFile();
    }
}
