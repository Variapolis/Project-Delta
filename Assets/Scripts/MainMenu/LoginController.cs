using Photon.Pun;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UniRx;
using UniRx.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
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
            var data = SaveDataManager.LoadPlayerData();
            var request = new LoginWithCustomIDRequest
            {
                CreateAccount = true,
                CustomId = data.guid
            };
            PlayFabClientAPI.LoginWithCustomID(request, PlayfabResultCallback, PlayfabErrorCallback);
            PhotonNetwork.LocalPlayer.NickName = nicknameField.text;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
            ShowWarning("Connecting...", Color.yellow);
        }

        public override void OnDisconnected(DisconnectCause cause) => ShowWarning("Connection Failed.", Color.red);

        private void ShowWarning(string text, Color textColor)
        {
            warningLabel.gameObject.SetActive(true);
            warningLabel.text = text;
            warningLabel.color = textColor;
        }

        private void PlayfabResultCallback(LoginResult result)
        {
            PlayFabClientAPI.UpdateUserTitleDisplayName(
                new UpdateUserTitleDisplayNameRequest { DisplayName = PhotonNetwork.LocalPlayer.NickName },
                NameUpdateCallback, PlayfabErrorCallback);
        }

        private void NameUpdateCallback(UpdateUserTitleDisplayNameResult result) =>
            Debug.Log($"Name Updated to {result.DisplayName}");

        private void PlayfabErrorCallback(PlayFabError error) => Debug.Log(error.ErrorMessage);
    }
}