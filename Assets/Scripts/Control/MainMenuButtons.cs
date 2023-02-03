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

    public void ClosePause()
    {
        SceneManager.UnloadSceneAsync("PauseMenu");
        Time.timeScale = 1;
    }
}
