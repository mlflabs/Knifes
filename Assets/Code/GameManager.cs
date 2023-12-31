using DG.Tweening;
using System;
using Unity.Services.Core;
using UnityEngine;

[Serializable]
public class GameData
{
    public int Level = 1;
    public int Score = 0;
    public int TopScore = 0;
    public String name;
}

[Serializable]
public class LevelSceneData
{
    //# array id is multiplierd by 10, so 0 = 0-9, 1 = 10-19
    public String[] scenes;
}


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }


    public GameData Data { get; private set; }

    public LevelSceneData[] SceneData;
    public GameState State { get; private set; }

    public string Test;


    public enum GameState
    {
        Loading,
        MainMenu,
        StartGame,
        Level,
        NextLevel,
        LevelResultFailed,
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            UnityServices.InitializeAsync();
        }




    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        Data = new GameData();
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        DOTween.KillAll();

        switch (State)
        {
            case GameState.Loading:
                break;
            case GameState.MainMenu:
                print("Going to start menu");
                SceneService.Instance.LoadScene("StartMenu");
                break;

            case GameState.StartGame:
                State = GameState.Level;
                Data.Level = 1;
                SceneService.Instance.LoadScene(SceneData[0].scenes[0]);
                break;

            case GameState.NextLevel:
                Data.Level += 1;
                //Data.Score = LevelStateSystem.Instance.score;
                int id = 0; //(int)(Data.Level * 0.1);
                var sceneData = SceneData[id];
                var rndSceneId = UnityEngine.Random.Range(0, sceneData.scenes.Length - 1);
                SceneService.Instance.LoadScene(sceneData.scenes[rndSceneId]);
                break;

            case GameState.LevelResultFailed:
                SceneService.Instance.LoadScene("Failed");
                break;

            default:
                Debug.LogError("Loaded Unknown Scene: " + State.ToString());
                break;
        }
    }




    public void StartGame()
    {
        UpdateGameState(GameState.StartGame);
    }



    public void NextLevel()
    {
        UpdateGameState(GameState.NextLevel);
    }

    public void BackToMainMenu()
    {
        UpdateGameState(GameState.MainMenu);
    }

    public void LevelCleared()
    {
        UISystem.Instance.PlayLevelClearedSummary();

    }

    public void LevelFailed()
    {
        UISystem.Instance.PlayLevelFinishedSummary();
    }

    public void setScore(int score)
    {
        Data.Score = score;
    }

    public void setLevel(int level)
    {
        Data.Level = level;
    }

    public void setName(String name)
    {
        Data.name = name;
    }

    public void setTopScore(int value)
    {
        Data.TopScore = value;
    }

    public int getLevel() => Data.Level;

    public int getTopScore() => Data.TopScore;
    public String getName() => Data.name;



}
