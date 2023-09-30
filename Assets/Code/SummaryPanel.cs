using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SummaryPanel : MonoBehaviour
{

    [SerializeField] private int _scorePerSecond = 10;

    public TextMeshProUGUI txtTime;


    public async void PlaySummary(TextMeshProUGUI score, GameObject[] items, int bonusTime)
    {
        txtTime.text = bonusTime.ToString();
        
        for(int i = 0; i < bonusTime; i++)
        {
            txtTime.text = (bonusTime - i).ToString();
            LevelStateSystem.Instance.AddScore(_scorePerSecond);
            await Task.Delay(200);
        }

        //add icons
    }


}
