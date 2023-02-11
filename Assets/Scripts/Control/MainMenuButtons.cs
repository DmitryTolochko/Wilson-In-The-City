using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void Reset()
    {
        GameModel.Instance.BlackScreen.SetActive(false);
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
        SoundPlayer.Instance.PlayMusic(MusicType.Game);
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
    }

    public void OpenShop()
    {
        CloseAllAsyncScenes(new string[] {"Game"});
        SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
        Time.timeScale = 0;
        SoundPlayer.Instance.PlayMusic(MusicType.Menu);
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
    }

    public void ExitGame()
    {
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
        Application.Quit();
    }

    public void OpenMenu()
    {
        CloseAllAsyncScenes(new string[] {"Game"});
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
        SoundPlayer.Instance.PlayMusic(MusicType.Menu);
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
    }

    public void OpenPause()
    {
        CloseAllAsyncScenes(new string[] {"Game"});
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
        SoundPlayer.Instance.PauseMusic();
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
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

        SoundPlayer.Instance.UnPauseMusic();
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
    }

    public void ClockButton()
    {
        StartCoroutine(GameModel.DecelerateGame());
        StartCoroutine(UI.StartClockTimer());
        GameModel.CollectedClocksCurrent -= 1;
        SoundPlayer.Instance.PlayUISound(UISoundType.ButtonClick);
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
}
