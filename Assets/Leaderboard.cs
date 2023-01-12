using PlayFab;
using UnityEngine;
using PlayFab.ClientModels;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardElementPrefab;
    [SerializeField] private RectTransform leaderboardContent;

    private void OnEnable() => GetLeaderboard();

    private void OnDisable() => leaderboardContent.DestroyChildren();

    private void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            MaxResultsCount = 20,
            StatisticName = "Kills",
            ProfileConstraints = new PlayerProfileViewConstraints { ShowDisplayName = true, ShowStatistics = true }
        };
        PlayFabClientAPI.GetLeaderboard(request, PrintLeaderboard, ShowError);
    }

    private void PrintLeaderboard(GetLeaderboardResult leaderboard)
    {
        foreach (var result in leaderboard.Leaderboard)
        {
            var element = Instantiate(leaderboardElementPrefab, leaderboardContent);
            element.GetComponent<LeaderboardElement>().Entry = result;
        }
    }

    private void ShowError(PlayFabError error) => Debug.Log($"Leaderboard Error: {error.ErrorMessage}");
}