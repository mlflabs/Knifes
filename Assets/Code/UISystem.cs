using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UISystem : MonoBehaviour
{

    [SerializeField] private Color _AddScoreCollor = Color.white;



    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtTime;

    [SerializeField] private GameObject _UICanvas;
    [SerializeField] private GameObject _itemsPannel;
    [SerializeField] private Apple _appleIcon;

    [SerializeField] private SummaryPanel _uiSummary;

    //TODO: have the buy buttons enable disable based on price, maybe a fillup effect
    private Color _originalScoreTextColor;
    private List<Apple> _items = new List<Apple>();

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
        txtScore.text = "Score: " + GameManager.Instance.Data.Score.ToString();
        _originalScoreTextColor = txtScore.color;
    }


    private void onTimeChanged(int value)
    {
        txtTime.text = "Bonus Time: " + value.ToString();
    }

    private Tween _scoreTweenText;
    private Tween _scoreTweenShake;
    //private Tween _scoreTweenColor;
    private int _currentScoreValue = 0;

    private void onScoreChanged(int value, bool animate)
    {
        if (!animate)
        {
            txtScore.text = "Score: " + value.ToString();
            _currentScoreValue = value;
            return;
        }

        txtScore.color = _AddScoreCollor;
        if (_scoreTweenText is not null && _scoreTweenText.active)
        {
            _scoreTweenText.Kill();
            _scoreTweenShake.Kill();
            //_scoreTweenColor.Kill();
        }
        var duration = (value > _currentScoreValue + 9) ? .8f : 0.3f;

        _scoreTweenText = DOTween.To(() => _currentScoreValue, x =>
        {
            _currentScoreValue = x;
            txtScore.text = "Score: " + x;
        }, value, duration);

        _scoreTweenShake = txtScore.transform.DOShakePosition(duration, 2f)
            .OnComplete(() => txtScore.color = _originalScoreTextColor);

    }

    public void AddApple()
    {
        print("UI, adding apple");
        var gb = Instantiate(_appleIcon, _itemsPannel.transform);
        gb.transform.DOShakePosition(1f);
        _items.Add(gb);
    }

    public Apple[] GetItems()
    {
        return _items.ToArray();
    }

    public void PlayLevelClearedSummary()
    {
        var summary = Instantiate(_uiSummary, _UICanvas.transform);
        summary.PlaySummary(LevelStateSystem.Instance.score, UISystem.Instance.GetItems(), LevelStateSystem.Instance.BonusTime);
    }
}
