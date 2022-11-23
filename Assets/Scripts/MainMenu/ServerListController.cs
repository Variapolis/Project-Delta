using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class ServerListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform serverListContent;
    [SerializeField] private GameObject serverElementPrefab;
    [SerializeField] private GameObject loadingBar;

    // public override void OnEnable() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        // loadingBar.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Refreshed Servers");
        serverListContent.DestroyChildren();
        foreach (var room in roomList)
        {
            var element = Instantiate(serverElementPrefab, serverListContent).GetComponent<ServerElement>();
            element.Room = room;
        }
    }
}