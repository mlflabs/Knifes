using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SummaryPanel : MonoBehaviour
{

    [SerializeField] private int _scorePerSecond = 5;

    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtTime;
    [SerializeField] private TextMeshProUGUI _txtLevel;

    [SerializeField] private GameObject _itemsParent;
    [SerializeField] private GameObject _btnNext;

    [SerializeField] private AudioClip _audioSuccess, _audioCoin;

    public async void PlaySummary(int level, int score, Apple[] items, int bonusTime)
    {

        AudioManager.Instance.PlaySound(_audioSuccess);
        
        _txtTime.text = bonusTime.ToString();
        _txtScore.text = score.ToString();
        _txtLevel.text = "Level: " + level.ToString();

        for (int i = 1; i <= bonusTime; i++)
        {
            _txtTime.text = (bonusTime - i).ToString();
            score += _scorePerSecond;
            _txtScore.text = score.ToString();
            //LevelStateSystem.Instance.AddScore(_scorePerSecond);
            //LevelStateSystem.Instance.eventBonusTimeChanged?.Invoke(bonusTime - i);
            AudioManager.Instance.PlaySound(_audioCoin);
            await UniTask.Delay(100);
        }

        //add icons
        for (var i = 0; i < items.Length; i++)
        {
            var a = Instantiate(items[i], _itemsParent.transform);
            score += a.ScoreAmount;
            _txtScore.text = score.ToString();
            //LevelStateSystem.Instance.AddScore(a.ScoreAmount);
            AudioManager.Instance.PlaySound(_audioCoin);
            await UniTask.Delay(500);
        }

        await UniTask.Delay(200);
        GameManager.Instance.setScore(score);
        _btnNext.SetActive(true);

    }

    public void LoadNextLevel()
    {
        GameManager.Instance.UpdateGameState(GameManager.GameState.NextLevel);
    }


}
