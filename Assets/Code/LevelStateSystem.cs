using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DG.Tweening;

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

    public int BonusTime = 20;

    private Image[] _knifeIcons;
    private int _usedKnifes;
    private List<Target> targers = new List<Target>();

    public UnityEvent<int, bool> eventScoreChanged = new UnityEvent<int, bool>();
    public UnityEvent<int> eventBonusTimeChanged = new UnityEvent<int>();
    public UnityEvent eventLevelFinished = new UnityEvent();

    public static LevelStateSystem Instance { get; private set; }
    public bool HasKnifes { get => _usedKnifes < _numberOfAvailableKnifes; }

    public bool LevelFinished = false;
    public bool LevelFailed = false;

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

    private void OnEnable()
    {
        print("OnEnablw------------------------");
    }

    private void Start()
    {

        print("OnStart =============================");
        prepareIcons();
        _usedKnifes = 0;
        score = GameManager.Instance.Data.Score;
        eventScoreChanged?.Invoke(score, false);
        setupBonusTimeCounter();
    }


    private async void setupBonusTimeCounter()
    {
        for (int i = 1; i <= BonusTime; i++)
        {
            await Task.Delay(1000);

            if (LevelFinished) return;
            print("Bonus Time:: " + BonusTime);
            BonusTime--;
            eventBonusTimeChanged?.Invoke(BonusTime);
        }
    }

    public void RegisterTarget(Target target)
    {
        targers.Add(target);
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

    public async void UseKnife()
    {
        if (_knifeIcons.Length > _usedKnifes)
            _knifeIcons[_usedKnifes].sprite = _knifeEmpty;
        _usedKnifes++;

        if (HasKnifes) return;
        await Task.Delay(200);

        if (LevelFailed) return; //prevent overlap

        PlaySuccessAnimation();

    }

    public async void PlaySuccessAnimation()
    {
        //Bug, if we throw too fast, the failed result will be overwritten by succcess
        if (LevelFailed) return;

        LevelFinished = true;
        eventLevelFinished?.Invoke();
        foreach (var target in targers)
        {
            target.PlayDestroyTargetAnimation();
        }
        await Task.Delay(500);
        GameManager.Instance.LevelCleared();
    }

    public void PlayFailedAnimation()
    {
        if (LevelFailed) return;//prevent overlap, 2 failed panels

        LevelFinished = true;
        LevelFailed = true;
        eventLevelFinished?.Invoke();
        GameManager.Instance.LevelFailed();
    }

    public void AddScore(int value)
    {
        score += value;
        eventScoreChanged.Invoke(score, true);
    }

    public void AddApple()
    {
        //_applesHit++;
        //AddScore(score);
        UISystem.Instance.AddApple();
    }

    //public void AddCredits(int value)
    //{
    //credits += value;
    //eventCreditChanged.Invoke(credits);
    //}


}
