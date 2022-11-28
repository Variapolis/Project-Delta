using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class ServerTeamBalancer : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient) SetPlayerTeam(PhotonNetwork.LocalPlayer);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient && !newPlayer.IsMasterClient) SetPlayerTeam(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient) otherPlayer.LeaveCurrentTeam();
    }

    void SetPlayerTeam(Player player)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        var spiesCount = PhotonTeamsManager.Instance.GetTeamMembersCount(1);
        var guardsCount = PhotonTeamsManager.Instance.GetTeamMembersCount(2);
        player.JoinTeam(spiesCount <= guardsCount ? (byte)1 : (byte)2);
    }
}