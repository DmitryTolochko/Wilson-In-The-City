using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour
{
    private static int collectedClocksCurrent;
    public GameObject GameOverWindow;

    public static GameModel Instance {get; private set;}

    public static int CrystalsCountCurrent;
    public static int MoneyCountCurrent;
    public static float ScoreCurrent;
    public static float TimeScaleCurrent;

    public static int CollectedClocksCurrent 
    { 
        get => collectedClocksCurrent; 
        set 
            {
                collectedClocksCurrent = value;
                IncreaseClockNum();
            } 
    }

    private void Start() 
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
        Instance = this;
    }

    private void Update() 
    {
        if (Time.timeScale >= 1)
        {
            ScoreCurrent += 0.05f;
            Time.timeScale += 0.0005f;
            PlayerModel.Instance.rb.gravityScale -= 0.001f;
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

    public static event Action IncreaseClockNum;

    public static IEnumerator DecelerateGame()
    {
        var gravity = PlayerModel.Instance.rb.gravityScale;
        TimeScaleCurrent = Time.timeScale;

        Time.timeScale = 1;
        PlayerModel.Instance.rb.gravityScale = 9.8f;

        yield return new WaitForSeconds(15);

        Time.timeScale = TimeScaleCurrent;
        PlayerModel.Instance.rb.gravityScale = gravity;
    }
}
