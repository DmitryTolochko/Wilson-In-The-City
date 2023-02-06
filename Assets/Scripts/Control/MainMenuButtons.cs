using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Reset()
    {
        CloseAllAsyncScenes("Game");

        ObstacleGenerator.DeleteAllObstacles();
        CollectableItemsGenerator.DeleteAllItems();
        PlayerModel.Reset();
        SceneryEngine.IsGameReseted = true;
        GameModel.ScoreCurrent = 0;
        GameModel.CrystalsCountCurrent = 0;
        GameModel.MoneyCountCurrent = 0;
        GameModel.CollectedClocksCurrent = 0;
        UI.StopClockTimer();
        if (GameModel.Instance != null && GameModel.Instance.GameOverWindow.activeSelf)
        {
            GameModel.Instance.GameOverWindow.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void OpenShop()
    {
        SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void OpenPause()
    {
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        CloseAllAsyncScenes("Game");
        
        if (GameModel.Instance.GameOverWindow.activeSelf)
        {
            ObstacleGenerator.DeleteAllObstacles();
            CollectableItemsGenerator.DeleteAllItems();
            GameModel.Instance.GameOverWindow.SetActive(false);
        }
        Time.timeScale = GameModel.TimeScaleCurrent;
    }

    public void ClockButton()
    {
        StartCoroutine(GameModel.DecelerateGame());
        StartCoroutine(UI.StartClockTimer());
        GameModel.CollectedClocksCurrent -= 1;
    }

    private void CloseAllAsyncScenes(string currentScene)
    {
        if (SceneManager.sceneCount > 1)
        {
            var scenes = SceneManager.GetAllScenes();
            foreach (var scene in scenes)
                if (scene.name != currentScene)
                    SceneManager.UnloadSceneAsync(scene);
        }
    }
}
