﻿using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerUIPrefab;
    [SerializeField] private Button startGameButton;

    public override void OnEnable()
    {
        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        if (PhotonNetwork.InRoom) RefreshPlayerList();
        base.OnEnable();
    }

    public override void OnJoinedRoom() => RefreshPlayerList();

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Joined");
        RefreshPlayerList();
    }

    private void RefreshPlayerList()
    {
        Debug.Log("Refreshed Servers");
        playerListContent.DestroyChildren();
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Instantiate(playerUIPrefab, playerListContent).GetComponent<PlayerUI>().Player = player.Value;
        }
    }
}