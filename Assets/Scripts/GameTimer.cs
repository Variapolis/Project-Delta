using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using TMPro;
using UniRx;
using UnityEngine;

public class GameTimer : MonoBehaviourPunCallbacks
{
    private const int GameTime = 300;
    [SerializeField] private TMP_Text timerText;
    private double _gameStartTime;
    private bool _hasStarted;
    private ReactiveProperty<int> _timeLeft;
    public IReadOnlyReactiveProperty<int> TimeLeft => _timeLeft;

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

        _timeLeft.Value = GameTime - Convert.ToInt32(PhotonNetwork.Time - _gameStartTime);
        timerText.text = SecondsToMinSec(_timeLeft.Value);
        if (_timeLeft.Value <= 0) enabled = false;
    }

    private static string SecondsToMinSec(int seconds) => $"{Mathf.Floor(seconds / 60f).ToString()}:{seconds % 60:00}";

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        var startTime = propertiesThatChanged["StartTime"];
        if (startTime == null) return;
        _gameStartTime = double.Parse(startTime.ToString());
        _hasStarted = true;
    }
}