using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class ServerListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform serverListContent;
    [SerializeField] private GameObject serverElementPrefab;
    [Inject] private ServerElement.Factory serverElementFactory;
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        transform.DestroyChildren();
        foreach (var room in roomList) serverElementFactory.Create(room);
    }
}