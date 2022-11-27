using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ScoreUIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text spyScoreText;
    [SerializeField] private TMP_Text guardScoreText;

    private void Start() => spyScoreText.text = guardScoreText.text = "0";

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        PhotonTeamsManager.Instance.TryGetTeamMembers(targetPlayer.GetPhotonTeam(), out var teamMembers);
        var teamScore = 0;
        foreach (var player in teamMembers) teamScore += player.GetScore();
        if (targetPlayer.GetPhotonTeam().Code == 1) spyScoreText.text = teamScore.ToString();
        else guardScoreText.text = teamScore.ToString();
    }
}
