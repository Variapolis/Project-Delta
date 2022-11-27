using System;
using System.Timers;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviourPunCallbacks
{
    private const int GameTime = 300;
    [SerializeField] private TMP_Text timerText;
    private double _gameStartTime;
    private bool _hasStarted;
    private bool finished;

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        var property = new Hashtable { { "StartTime", PhotonNetwork.Time } };
        PhotonNetwork.CurrentRoom.SetCustomProperties(property);
    }

    private void Update()
    {
        if (!_hasStarted)
        {
            timerText.text = SecondsToMinSec(GameTime);
            return;
        }
        
        var timeLeft = GameTime - Convert.ToInt32(PhotonNetwork.Time - _gameStartTime);
        timerText.text = SecondsToMinSec(timeLeft);
        if (timeLeft <= 0)
        {
            enabled = false;
            // TODO: End Game Panel appears
        }
    }

    private static string SecondsToMinSec(int seconds) => $"{Mathf.Floor(seconds / 60f).ToString()}:{seconds % 60:00}";

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        var startTime = propertiesThatChanged["StartTime"];
        if(startTime == null) return;
        _gameStartTime = double.Parse(startTime.ToString());
        _hasStarted = true;
    }
}