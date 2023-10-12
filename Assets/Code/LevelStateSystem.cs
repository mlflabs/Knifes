using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class LevelStateSystem : MonoBehaviour
{

    public float scoreIncrementIntervalInSec = 1f;
    public int scoreIncrementValue = 1;


    public int score { get; private set; }

    //public int credits { get; private set; }



    [SerializeField] private int _numberOfAvailableKnifes;
    public int TotalKnifes { get => _numberOfAvailableKnifes;  }
    private int _usedKnifes;

    public int BonusTime = 20;

    
    
    private List<Target> targers = new List<Target>();

    public UnityEvent<int, bool> eventScoreChanged = new UnityEvent<int, bool>();
    public UnityEvent<int> eventBonusTimeChanged = new UnityEvent<int>();
    public UnityEvent eventLevelFinished = new UnityEvent();
    public UnityEvent eventKnifeThrown = new UnityEvent();

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

    private void Start()
    {
        _usedKnifes = 0;
        score = GameManager.Instance.Data.Score;
        eventScoreChanged?.Invoke(score, false);
        setupBonusTimeCounter();
    }


    private async void setupBonusTimeCounter()
    {
        for (int i = 1; i <= BonusTime; i++)
        {
            await UniTask.Delay(1000);

            if (LevelFinished) return;
            BonusTime--;
            eventBonusTimeChanged?.Invoke(BonusTime);
        }
    }

    public void RegisterTarget(Target target)
    {
        targers.Add(target);
    }


    public async void UseKnife()
    {
        _usedKnifes++;
        eventKnifeThrown?.Invoke();

        if (HasKnifes) return;
        await UniTask.Delay(200);

        if (LevelFailed) return;
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
        await UniTask.Delay(500);
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
