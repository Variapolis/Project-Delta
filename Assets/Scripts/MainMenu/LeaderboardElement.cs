using System.Linq;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text deathsText;
    [SerializeField] private TMP_Text kdText;
    private PlayerLeaderboardEntry _entry;

    public PlayerLeaderboardEntry Entry
    {
        get => _entry;
        set
        {
            _entry = value;
            var latestVersion = value.Profile.Statistics.Select(s => s.Version).Max();
            playerNameText.text = value.Profile.DisplayName;
            foreach (var statisticModel in value.Profile.Statistics)
            {
                if (statisticModel.Version != latestVersion) continue;
                switch (statisticModel.Name)
                {
                    case "Kills":
                        killsText.text = statisticModel.Value.ToString();
                        break;
                    case "Deaths":
                        deathsText.text = statisticModel.Value.ToString();
                        break;
                    case "KD Ratio":
                        kdText.text = statisticModel.Value.ToString();
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}