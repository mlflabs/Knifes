using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UISystem : MonoBehaviour
{

    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtCredits;

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
        GameStateSystem.Instance.eventCreditChanged.AddListener(onCreditChanged);
        GameStateSystem.Instance.eventScoreChanged.AddListener(onScoreChanged);
    }

    private void onCreditChanged(int value)
    {
        txtCredits.text = "Credits: " + value.ToString();
    }

    private void onScoreChanged(int value)
    {
        txtScore.text = "Score: " + value.ToString();
    }
}
