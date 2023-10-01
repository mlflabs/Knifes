using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SummaryPanel : MonoBehaviour
{

    [SerializeField] private int _scorePerSecond = 5;

    [SerializeField] private TextMeshProUGUI _txtTime;
    [SerializeField] private TextMeshProUGUI _txtLevel;

    [SerializeField] private GameObject _itemsParent;
    [SerializeField] private GameObject _btnNext;

    public async void PlaySummary(TextMeshProUGUI score, Apple[] items, int bonusTime)
    {
        _txtTime.text = bonusTime.ToString();
        
        for(int i = 1; i <= bonusTime; i++)
        {
            _txtTime.text = (bonusTime - i).ToString();
            LevelStateSystem.Instance.AddScore(_scorePerSecond);
            LevelStateSystem.Instance.eventBonusTimeChanged?.Invoke(bonusTime - i);
            await Task.Delay(100);
        }

        //add icons
        for(var i = 0; i < items.Length; i++)
        {
            var a = Instantiate(items[i], _itemsParent.transform);
            LevelStateSystem.Instance.AddScore(a.ScoreAmount);
            await Task.Delay(500);
        }

        await Task.Delay(1000);
        _btnNext.SetActive(true);

    }

    public void LoadNextLevel()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.NextLevel);
    }


}
