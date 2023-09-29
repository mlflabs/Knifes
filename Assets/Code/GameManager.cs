using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[Serializable]
public class GameData
{
    public int Level;
    public int Score;
}


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public GameData Data { get; private set; }

    
    public GameState State { get; private set; }



    public enum GameState
    {
        Loading,
        MainMenu,
        StartGame,
        Level,
        NextLevel,
        LevelResultSuccess,
        LevelResultFailed,
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

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
                SceneService.Instance.LoadScene("StartMenu");
                break;

            case GameState.StartGame:
                State = GameState.Level;
                Data.Level = 1;
                SceneService.Instance.LoadScene("Level");
                break;

            case GameState.NextLevel:
                Data.Level += 1;
                SceneService.Instance.LoadScene("Level");
                break;

            case GameState.LevelResultSuccess:
                
                SceneService.Instance.LoadScene("Success");
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

}
