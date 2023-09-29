using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class LevelStateSystem : MonoBehaviour
{

    public float scoreIncrementIntervalInSec = 1f;
    public int scoreIncrementValue = 1;


    public int score { get; private set; }

    //public int credits { get; private set; }



    [SerializeField] private int _numberOfAvailableKnifes;
    [SerializeField] private GameObject _knifePannel;
    [SerializeField] private Image _knifeIcon;
    [SerializeField] private Sprite _knifeFull;
    [SerializeField] private Sprite _knifeEmpty;

    private Image[] _knifeIcons;
    private int _usedKnifes;

    public UnityEvent<int> eventScoreChanged = new UnityEvent<int>();
    public UnityEvent<float> eventBonusTimeChanged = new UnityEvent<float>();  

    public static LevelStateSystem Instance { get; private set; }
    public bool HasKnifes { get => _usedKnifes < _numberOfAvailableKnifes;}

    //private float currentTime = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
            Instance = this;

    }

    private void Start()
    {

        prepareIcons();
        _usedKnifes = 0;
        
    }


    private void prepareIcons()
    {
        _knifeIcons = new Image[_numberOfAvailableKnifes];
        for (var i = 0; i < _numberOfAvailableKnifes; i++)
        {
            var icon = Instantiate(_knifeIcon);
            icon.sprite = _knifeFull;
            icon.transform.SetParent(_knifePannel.transform);
            _knifeIcons[i] = icon;

        }
    }

    public void UseKnife()
    {

        _knifeIcons[_usedKnifes].sprite = _knifeEmpty;
        _usedKnifes++;

        if (HasKnifes) return;

        GameManager.Instance.UpdateGameState(GameManager.GameState.LevelResultSuccess);
    }

    public void ThrowFailed()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.LevelResultFailed);
    }

    public void AddScore(int value)
    {
        score += value;
        eventScoreChanged.Invoke(score);
    }

    //public void AddCredits(int value)
    //{
        //credits += value;
        //eventCreditChanged.Invoke(credits);
    //}

    
}
