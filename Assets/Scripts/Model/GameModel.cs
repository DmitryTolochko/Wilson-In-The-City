using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameModelStruct
{
    public int MoneyCount; 
    public int CrystalCount;
    public float BestScore;
    public SkinType SkinType;
    public List<SkinType> OpenedTypes;
}

public class GameModel : MonoBehaviour
{
    public GameObject BlackScreen;
    private static int collectedClocksCurrent;
    public GameObject GameOverWindow;

    public static GameModel Instance {get; private set;}

    [SerializeField]
    public static int CrystalsCountCurrent, MoneyCountCurrent, MoneyCount, CrystalCount;
    [SerializeField]
    public static float ScoreCurrent, TimeScaleCurrent, BestScore;
    [SerializeField]
    public static SkinType SkinType;
    public static List<SkinType> OpenedCharacters;

    [SerializeField]
    private static string savePath;
    [SerializeField]
    private string saveFileName = "data.json";

    public static int CollectedClocksCurrent 
    { 
        get => collectedClocksCurrent; 
        set 
            {
                collectedClocksCurrent = value;
                IncreaseClockNum();
            } 
    }

    public static void SaveToFile()
    {
        var game = new GameModelStruct
        {
            MoneyCount = MoneyCount,
            CrystalCount = CrystalCount,
            BestScore = BestScore,
            SkinType = SkinType,
            OpenedTypes = OpenedCharacters
        };

        var json = JsonUtility.ToJson(game, true);

        File.WriteAllText(savePath, json);
    }

    public void LoadFromFile()
    {
        if (!File.Exists(savePath))
            return;
        var json = File.ReadAllText(savePath);

        var game = JsonUtility.FromJson<GameModelStruct>(json);

        MoneyCount = game.MoneyCount;
        CrystalCount = game.CrystalCount;
        BestScore = game.BestScore;
        SkinType = game.SkinType;
        OpenedCharacters = game.OpenedTypes;
    }

    private void Awake() 
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif
        LoadFromFile();
    }

    private void OnApplicationQuit() 
    {
        SaveToFile();
    }

    private void OnApplicationPause(bool pauseStatus) 
    {
        if (Application.platform == RuntimePlatform.Android)
            SaveToFile();
    }

    private void Start() 
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        Time.timeScale = 0;
        Instance = this;
        SoundPlayer.Instance.PlayMusic(MusicType.Menu);
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

    public static void StartGameOverRoutine()
    {
        TimeScaleCurrent = Time.timeScale;
        Time.timeScale = 0;
        SoundPlayer.Instance.StopMusic();
        SoundPlayer.Instance.PlayUISound(UISoundType.GameOver);

        if (BestScore < ScoreCurrent)
            BestScore = ScoreCurrent;

        MoneyCount += MoneyCountCurrent;
        CrystalCount += CrystalsCountCurrent;

        SaveToFile();
        Instance.GameOverWindow.SetActive(true);
        GameOverNumbers.Refresh();
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
