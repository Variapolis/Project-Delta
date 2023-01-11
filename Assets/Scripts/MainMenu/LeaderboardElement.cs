using PlayFab.ClientModels;
using TMPro;
using Unity.VisualScripting;
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
            playerNameText.text = value.DisplayName;
            killsText.text = value.Profile.Statistics[0].Value.ToString();
            deathsText.text = value.Profile.Statistics[1].Value.ToString();
            kdText.text = value.Profile.Statistics[2].Value.ToString();
        }
    }
}