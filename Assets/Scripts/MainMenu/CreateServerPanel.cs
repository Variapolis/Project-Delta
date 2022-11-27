using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CreateServerPanel : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createServerButton;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Dropdown maxPlayers;
    [SerializeField] private Toggle joinInProgress; // TODO: Add JIP Functionality

    private void Start()
    {
        createServerButton.onClick
            .AsObservable()
            .Where(_ => nameInput.text != string.Empty)
            .Subscribe(_ => CreateRoom())
            .AddTo(gameObject);

        nameInput.onEndEdit
            .AsObservable()
            .Where(t => t == string.Empty)
            .Subscribe()
            .AddTo(gameObject);
    }

    private void CreateRoom()
    {
        var roomOptions = new RoomOptions
        {
            IsVisible = true,
            MaxPlayers = byte.Parse(maxPlayers.options[maxPlayers.value].text)
        };
        PhotonNetwork.CreateRoom(nameInput.text, roomOptions);
        gameObject.SetActive(false);
    }
}