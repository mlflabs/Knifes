using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Services.Core;
using System.Threading.Tasks;
using System;

[Serializable]
public class PlayerScoreUI
{
    [SerializeField] public TextMeshProUGUI TxtName;
    [SerializeField] public TextMeshProUGUI TxtScore;
}
public class LeaderBoardUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _txtPlayerName;
    [SerializeField] private TextMeshProUGUI _txtPlayerScore;

    [SerializeField] private PlayerScoreUI[] _topScores;

    [SerializeField] private Color32 _highlighColor;

    async void Start()
    {
        await UnityGameServices.Authenticate();
        await updatePlayerInfo();
        await updateTopScores();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private async Task updateTopScores()
    {
        var scores = await UnityGameServices.GetScores();

        var total = (scores.Results.Count < _topScores.Length) ? scores.Results.Count : _topScores.Length;
        for (var i = 0; i < total; i++)
        {

            var player = scores.Results[i];
            var x = _topScores[i];

            x.TxtScore.text = player.Score + " (" + (player.Rank + 1).ToString() + ")";
            if (player.PlayerName == GameManager.Instance.getName())
            {
                x.TxtName.text = "** You:";
                x.TxtName.color = _highlighColor;
            }
            else
            {
                x.TxtName.text = player.PlayerName + ":";
            }


        }
    }

    private async Task updatePlayerInfo()
    {
        var entry = await UnityGameServices.GetPlayerScore();
        if (entry is null)
        {
            _txtPlayerName.text = "Guest";
            _txtPlayerScore.text = "0";
            return;
        }
        //_txtPlayerName.text = entry.PlayerName;
        _txtPlayerScore.text = entry.Score.ToString() + " (" + (entry.Rank + 1).ToString() + ")";

        GameManager.Instance.setTopScore((int)entry.Score);
        GameManager.Instance.setName(entry.PlayerName);
    }
}
