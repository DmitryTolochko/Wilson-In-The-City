using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Reset()
    {
        CloseAllAsyncScenes(new string[] {"Game"});

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
        CloseAllAsyncScenes(new string[] {"Game"});
        SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenMenu()
    {
        CloseAllAsyncScenes(new string[] {"Game"});
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void OpenPause()
    {
        CloseAllAsyncScenes(new string[] {"Game"});
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        CloseAllAsyncScenes(new string[] {"Game"});
        
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

    private void CloseAllAsyncScenes(string[] currentScenes)
    {
        if (SceneManager.sceneCount > 1)
        {
            var scenes = SceneManager.GetAllScenes();
            foreach (var scene in scenes)
                if (!currentScenes.Contains(scene.name))
                    SceneManager.UnloadSceneAsync(scene);
        }
    }

    // public void ChangeSkinToWilson()
    // {
    //     PlayerAnimator.ChangeSkin(SkinType.Wilson);
    // }

    // public void ChangeSkinToRichard()
    // {
    //     PlayerAnimator.ChangeSkin(SkinType.Richard);
    // }

    // public void ChangeSkinToMarvin()
    // {
    //     PlayerAnimator.ChangeSkin(SkinType.Marvin);
    // }
}
