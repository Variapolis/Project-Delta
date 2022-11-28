using Photon.Pun;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button startButton;

    private void Reset() => startButton = GetComponent<Button>();

    private void Start() => startButton.onClick
        .AsObservable()
        .Where(_ => PhotonNetwork.IsMasterClient)
        .Subscribe(_ => StartGame())
        .AddTo(gameObject);

    private void StartGame()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("OnlinePlay");
    }
}