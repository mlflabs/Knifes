using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using System;
using Unity.Services.Leaderboards.Models;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Cysharp.Threading.Tasks;

public class UnityGameServices
{

    const string leaderboardId = "knifes";

    public static async void AddScoreToAnonymouse(float score)
    {
        Debug.Log("Saving user score:: " + score);
        try
        {
            await UnityServices.InitializeAsync();
            await Authenticate();
            await AddScore(score);
            await GetScores();

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

    }

    public static async UniTask<String> AnonymousAuthenticateAndGetName()
    {
        return await Authenticate();
    }
    public static async UniTask<String> UpdateName(String name)
    {
        try
        {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }


        return AuthenticationService.Instance.PlayerName;
    }
    public static async UniTask<String> Authenticate()
    {
        try
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
                await UnityServices.InitializeAsync();
            if (!AuthenticationService.Instance.IsSignedIn)
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"PlayerName: {AuthenticationService.Instance.PlayerName}");
            return AuthenticationService.Instance.PlayerName;

            //TODO:: save id.....

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        return "Guest";
    }

    public static async UniTask AddScore(double score)
    {
        var playerEntry = await LeaderboardsService.Instance
            .AddPlayerScoreAsync(leaderboardId, score);
    }


    public static async UniTask<LeaderboardEntry> GetPlayerScore()
    {

        try
        {
            var res = await LeaderboardsService.Instance.GetPlayerScoreAsync(leaderboardId);
            return res;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return null;
    }

    public async void GetPaginatedScores()
    {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(
            leaderboardId,
            new GetScoresOptions { Offset = 25, Limit = 50 }
        );
    }
    public static async UniTask<LeaderboardScoresPage> GetScores(int offset = 0, int limit = 3)
    {
        try
        {
            var res = await LeaderboardsService.Instance.GetScoresAsync(
                leaderboardId,
                new GetScoresOptions { Offset = offset, Limit = limit }
            );
            return res;
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return null;
    }


    public static void SendLevelClearedEvent(int level, int score)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "userScore", score },
            { "userLevel", level },
        };

        AnalyticsService.Instance.CustomData("LevelFinished", parameters);
    }
}