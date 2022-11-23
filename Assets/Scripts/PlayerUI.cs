﻿using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text playerIDText;
    [SerializeField] private TMP_Text playerLatencyText;

    private Player _player;

    public Player Player
    {
        get => _player;
        set
        {
            _player = value;
            nameText.text = value.NickName;
            playerIDText.text = _player.ActorNumber.ToString();
            if(Equals(value, PhotonNetwork.LocalPlayer)) nameText.color = Color.yellow;
            // playerLatencyText.text = PhotonNetwork.GetPing().ToString();
        }
    }
}