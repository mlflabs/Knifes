using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FailedPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore;
    [SerializeField] private TextMeshProUGUI _txtLevel;

    [SerializeField] private TextMeshProUGUI _txtTopScore;
    [SerializeField] private GameObject _btnNext;
    [SerializeField] private AudioClip _audioFailed;

    public async void PlayFailedSummary(int level, int score)
    {

        AudioManager.Instance.PlaySound(_audioFailed);

        _txtScore.text = score.ToString();
        _txtLevel.text = level.ToString();

        if (score > GameManager.Instance.getTopScore())
        {
            await Task.Delay(200);
            /// _txtTopScore.transform.localScale = new Vector3(5f, 5f, 1);
            _txtTopScore.gameObject.SetActive(true);
            // _txtTopScore.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
            await Task.Delay(300);
        }

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
