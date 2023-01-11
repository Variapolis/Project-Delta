using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayerStatsUpdater : MonoBehaviour
{
    void OnEnable()
    {
        var data = SaveDataManager.LoadPlayerData();
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new()
                {
                    StatisticName = "Kills",
                    Value = 15
                },
                new()
                {
                    StatisticName = "Deaths",
                    Value = 5
                },
                new()
                {
                    StatisticName = "KD Ratio",
                    Value = 15
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, StatisticsResultCallback, StatisticsErrorCallback);
    }

    private void StatisticsResultCallback(UpdatePlayerStatisticsResult result) => Debug.Log("Stats updated");

    private void StatisticsErrorCallback(PlayFabError error) => Debug.Log($"Stats update failed: {error.ErrorMessage}");
}