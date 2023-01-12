using System;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UniRx;
using UnityEngine;

public class Chat : MonoBehaviourPunCallbacks, IChatClientListener
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text chatContent;

    private string _userName;
    private ChatClient _chatClient;

    private void Awake()
    {
        Debug.Log("Ran");
        inputField.onSubmit.AsObservable().Subscribe(SendMessage).AddTo(gameObject);
        _chatClient = new ChatClient(this);
    }

    private void Update() => _chatClient.Service();

    private void SendMessage(string message)
    {
        if (inputField.text == string.Empty) return;
        _chatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, inputField.text);
        inputField.text = string.Empty;
    }

    private void OnEnable() => JoinChat();

    public override void OnJoinedRoom() => JoinChat();


    public override void OnLeftRoom() => LeaveChat();


    private void JoinChat()
    {
        if (_userName != null || !PhotonNetwork.InRoom) return;
        _userName = PhotonNetwork.LocalPlayer.NickName;
        _chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, "1.0",
            new AuthenticationValues(_userName));
    }

    private void LeaveChat() => _chatClient.Disconnect();

    public void DebugReturn(DebugLevel level, string message) => Debug.Log($"Chat: {level} - {message}");

    public void OnDisconnected() => Debug.Log("Chat Disconnected");

    public new void OnConnected()
    {
        Debug.Log("Chat Connected");
        _chatClient.Subscribe(PhotonNetwork.CurrentRoom.Name,
            creationOptions: new ChannelCreationOptions { PublishSubscribers = true });
    }

    public void OnChatStateChange(ChatState state) => Debug.Log($"Chat State changed to {state}");

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (_chatClient.TryGetChannel(PhotonNetwork.CurrentRoom.Name, out var chat))
            chatContent.text = chat.ToStringMessages(); // BUG: Chat gets cut off after word limit and doesn't reset when a room is ended
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
            if (results[i])
                _chatClient.PublishMessage(PhotonNetwork.CurrentRoom.Name, " joined the chat!");
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }
}