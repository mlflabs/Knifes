using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateSystem : MonoBehaviour
{

    public float scoreIncrementIntervalInSec = 1f;
    public int scoreIncrementValue = 1;


    public int score { get; private set; }
    public int credits { get; private set; }
    


    public UnityEvent<int> eventScoreChanged = new UnityEvent<int>();
    public UnityEvent<int> eventCreditChanged = new UnityEvent<int>();  

    public static GameStateSystem Instance { get; private set; }

    //private float currentTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }


    private void Update()
    {


        //For incremental updates, not using it
        /*
        currentTime += Time.deltaTime;

        if (currentTime > scoreIncrementIntervalInSec)
        {
            currentTime -= scoreIncrementIntervalInSec;
            addCredits(scoreIncrementValue);
        }
        */
    }


    public void addScore(int value)
    {
        score += value;
        eventScoreChanged.Invoke(score);
    }

    public void addCredits(int value)
    {
        credits += value;
        eventCreditChanged.Invoke(credits);
    }


}
