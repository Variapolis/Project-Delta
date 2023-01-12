using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using PlayFab;
using PlayFab.ClientModels;
using UniRx;
using UnityEngine;

public class EndGameDataHandler : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;

    void Start() =>
        gameTimer.TimeLeft
            .Where(t => t <= 0).Skip(1)
            .Subscribe(_ => UpdateStats())
            .AddTo(gameObject);

    private void UpdateStats()
    {
        var playerData = SaveDataManager.LoadPlayerData();
        var deaths = (int)PhotonNetwork.LocalPlayer.CustomProperties["Deaths"];
        var kills = PhotonNetwork.LocalPlayer.GetScore();
        playerData.kills += kills;
        playerData.deaths += deaths;
        playerData.kdRatio =  playerData.deaths == 0 ? playerData.kills :  playerData.kills /  playerData.deaths;
        SaveDataManager.SavePlayerData(playerData);
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new() { StatisticName = "Kills", Value = playerData.kills },
                new() { StatisticName = "Deaths", Value = playerData.deaths },
                new() { StatisticName = "KD Ratio", Value = playerData.kdRatio }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, StatisticsResultCallback, StatisticsErrorCallback);
    }

    private void StatisticsResultCallback(UpdatePlayerStatisticsResult result) => Debug.Log("Stats updated");

    private void StatisticsErrorCallback(PlayFabError error) => Debug.Log($"Stats update failed: {error.ErrorMessage}");
}