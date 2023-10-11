using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Codice.CM.Client.Differences.Merge;
using PlasticPipe.PlasticProtocol.Messages;
using NUnit.Framework;
using Unity.Services.Leaderboards.Models;

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

    public static async Task<String> AnonymousAuthenticateAndGetName()
    {
        return await Authenticate();
    }
    public static async Task<String> UpdateName(String name)
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
    public static async Task<String> Authenticate()
    {
        try
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
                await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");

            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"PlayerName: {AuthenticationService.Instance.PlayerName}");
            return AuthenticationService.Instance.PlayerName;

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

    public static async Task AddScore(double score)
    {
        var playerEntry = await LeaderboardsService.Instance
            .AddPlayerScoreAsync(leaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(playerEntry));
    }


    public static async Task<LeaderboardEntry> GetPlayerScore()
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
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }
    public static async Task<LeaderboardScoresPage> GetScores(int offset = 0, int limit = 3)
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
}