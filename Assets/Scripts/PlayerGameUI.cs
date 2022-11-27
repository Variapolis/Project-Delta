using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerGameUI : PlayerUIElement
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text playerIDText;
    [SerializeField] private TMP_Text playerLatencyText;

    private Player _player;

    public override Player Player
    {
        get => _player;
        set
        {
            _player = value;
            nameText.text = value.NickName;
            playerIDText.text = _player.ActorNumber.ToString();
            scoreText.text = value.GetScore().ToString();
            if(Equals(value, PhotonNetwork.LocalPlayer)) nameText.color = Color.yellow;
            playerLatencyText.text = "N/A";
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(Equals(targetPlayer, _player)) scoreText.text = targetPlayer.GetScore().ToString();
    }
}