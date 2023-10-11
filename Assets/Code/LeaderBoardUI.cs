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

    [SerializeField] private Color _highlighColor;


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

        for (var i = 0; i < scores.Results.Count || i < _topScores.Length; i++)
        {
            var player = scores.Results[i];
            var x = _topScores[i];
            x.TxtName.text = player.PlayerName;
            x.TxtScore.text = player.Score + " (" + (player.Rank + 1).ToString() + ")";
            if (x.TxtName.text == _txtPlayerName.text)
            {
                x.TxtName.color = _highlighColor;
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
        _txtPlayerName.text = entry.PlayerName;
        _txtPlayerScore.text = entry.Score.ToString() + " (" + (entry.Rank + 1).ToString() + ")";
    }
}
