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
        if (!newPlayer.IsMasterClient && newPlayer.GetPhotonTeam() == null) SetPlayerTeam(newPlayer);
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
        Debug.Log(PhotonTeamsManager.Instance.GetAvailableTeams());
        player.JoinTeam(spiesCount <= guardsCount ? (byte)1 : (byte)2);
    }
}