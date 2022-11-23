using System;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class LoginController : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField nicknameField;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text warningLabel;

    private void Start()
    {
        warningLabel.text = string.Empty;

        loginButton
            .OnClickAsObservable()
            .Where(_ => !PhotonNetwork.IsConnectedAndReady && nicknameField.text != string.Empty)
            .Debug()
            .Subscribe(_ => TryLogin())
            .AddTo(gameObject);

        nicknameField.onEndEdit
            .AsObservable()
            .Debug()
            .Subscribe(_ =>
            {
                if (nicknameField.text == string.Empty) ShowWarning("Name cannot be empty.", Color.red);
                else warningLabel.text = string.Empty;
            })
            .AddTo(gameObject);
    }

    private void TryLogin()
    {
        PhotonNetwork.LocalPlayer.NickName = nicknameField.text;
        PhotonNetwork.ConnectUsingSettings();
        ShowWarning("Connecting...", Color.yellow);
    }

    public override void OnDisconnected(DisconnectCause cause) => ShowWarning("Connection Failed.", Color.red);

    private void ShowWarning(string text, Color textColor)
    {
        warningLabel.gameObject.SetActive(true);
        warningLabel.text = text;
        warningLabel.color = textColor;
    }
}