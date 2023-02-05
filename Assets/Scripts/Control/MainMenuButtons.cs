using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Reset()
    {
        ObstacleGenerator.DeleteAllObstacles();
        PlayerModel.Reset();
        SceneryEngine.IsGameReseted = true;
        if (GameModel.Instance != null && GameModel.Instance.GameOverWindow.activeSelf)
        {
            GameModel.Instance.GameOverWindow.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenPause()
    {
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        if (SceneManager.sceneCount > 1)
            SceneManager.UnloadSceneAsync("PauseMenu");
        if (GameModel.Instance.GameOverWindow.activeSelf)
        {
            ObstacleGenerator.DeleteAllObstacles();
            GameModel.Instance.GameOverWindow.SetActive(false);
        }
        PlayerModel.Instance.rb.gravityScale = 9.8f;
        PlayerModel.Instance.rb.mass = 0.8f;
        Time.timeScale = 1;
    }
}
