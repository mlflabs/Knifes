using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FailedPanel : MonoBehaviour
{

    [SerializeField] private int _scorePerSecond = 5;

    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtLevel;

    [SerializeField] private GameObject _btnNext;

    [SerializeField] private AudioClip _audioFailed;

    public async void PlayFailedSummary(int level, int score)
    {

        AudioManager.Instance.PlaySound(_audioFailed);

        _txtScore.text = score.ToString();
        _txtLevel.text = "Level: " + level.ToString();



        await Task.Delay(200);
        GameManager.Instance.setScore(score);
        UnityGameServices.AddScoreToAnonymouse(score);
        _btnNext.SetActive(true);

    }

    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }


}
