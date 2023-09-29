using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UISystem : MonoBehaviour
{

    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtTime;

    //TODO: have the buy buttons enable disable based on price, maybe a fillup effect


    public static UISystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }

    public void Start()
    {
        LevelStateSystem.Instance.eventBonusTimeChanged.AddListener(onTimeChanged);
        LevelStateSystem.Instance.eventScoreChanged.AddListener(onScoreChanged);

        txtLevel.text = "Level: " + GameManager.Instance.Data.Level.ToString();
    }


    private void onTimeChanged(float value)
    {
        txtTime.text = "Bonus Time: " + value.ToString();
    }

    private void onScoreChanged(int value)
    {
        txtScore.text = "Score: " + value.ToString();
    }
}
