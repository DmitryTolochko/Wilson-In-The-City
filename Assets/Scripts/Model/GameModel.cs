using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour
{
    public GameObject GameOverWindow;

    public static GameModel Instance {get; private set;}

    public static int CrystalsCountCurrent;
    public static int MoneyCountCurrent;
    public static float ScoreCurrent;
    public static float TimeScaleCurrent;

    private void Start() 
    {
        Instance = this;
    }

    private void Update() 
    {
        if (Time.timeScale >= 1)
        {
            ScoreCurrent += 0.05f;
            Time.timeScale += 0.0005f;
            PlayerModel.Instance.rb.gravityScale -= 0.0005f;
            //PlayerModel.Instance.rb.mass += 0.0001f;
        }
    }

    public static IEnumerator StartGameOverRoutine()
    {
        TimeScaleCurrent = Time.timeScale;
        Time.timeScale = 0;
        Instance.GameOverWindow.SetActive(true);
        yield return 0;
    }
}
