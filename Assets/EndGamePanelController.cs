using Photon.Pun;
using Photon.Pun.UtilityScripts;
using TMPro;
using UniRx;
using UnityEngine;

public class EndGamePanelController : MonoBehaviour
{
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text spyScoreText;
    [SerializeField] private TMP_Text guardScoreText;

    void Start() =>
        gameTimer.TimeLeft
            .Do(_ => gameObject.SetActive(false)).Where(t => t <= 0)
            .Subscribe(_ =>
            {
                gameObject.SetActive(true);
                UpdateMessage();
            })
            .AddTo(gameObject);

    private void UpdateMessage()
    {
        PhotonTeamsManager.Instance.TryGetTeamMembers(1, out var spies);
        PhotonTeamsManager.Instance.TryGetTeamMembers(2, out var guards);
        int spyScore = 0;
        foreach (var spy in spies) spyScore += spy.GetScore();
        int guardScore = 0;
        foreach (var guard in guards) guardScore += guard.GetScore();
        spyScoreText.text = spyScore.ToString();
        guardScoreText.text = guardScore.ToString();
        messageText.text = GetVictoryMessage(spyScore, guardScore);
    }

    private static string GetVictoryMessage(int spyScore, int guardScore)
    {
        if (spyScore == guardScore) return "Tie";
        if (PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1 && spyScore > guardScore ||
            PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 2 && guardScore > spyScore) return "Victory";
        return "Defeat";
    }
}