using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModel : MonoBehaviour
{
    public GameObject GameOverWindow;

    public static GameModel Instance {get; private set;}

    private void Start() 
    {
        Instance = this;
    }

    private void Update() 
    {
        if (Time.timeScale >= 1)
        {
            Time.timeScale += 0.0005f;
            PlayerModel.Instance.rb.gravityScale -= 0.0005f;
            PlayerModel.Instance.rb.mass += 0.0001f;
        }
    }

    public static IEnumerator StartGameOverRoutine()
    {
        Time.timeScale = 0;
        Instance.GameOverWindow.SetActive(true);
        yield return 0;
    }
}
