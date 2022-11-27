using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TrainingButtonHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button button;
    private bool _clicked;

    private void Reset() => button = GetComponent<Button>();

    void Start() => button.onClick
        .AsObservable()
        .Subscribe(_ =>
        {
            string roomName = $"{PhotonNetwork.LocalPlayer.NickName}'s Training";
            roomName = roomName.Equals(string.Empty) ? "Room " + Random.Range(1000, 10000) : roomName;

            RoomOptions options = new RoomOptions
                { MaxPlayers = 1, PlayerTtl = 10000, IsVisible = false, IsOpen = false };
            _clicked = true;
            Debug.Log("Training Click");
            PhotonNetwork.CreateRoom(roomName, options);
        })
        .AddTo(gameObject);


    public override void OnCreatedRoom()
    {
        if (!_clicked) return;
        Invoke(nameof(SetupTraining), 0.2f);
        
    }

    private void SetupTraining() => PhotonNetwork.LoadLevel("Training");
}