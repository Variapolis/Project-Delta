using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CreateServerButton : MonoBehaviourPunCallbacks
{
    public void CreateRoom()
    {
        var roomOptions = new RoomOptions
        {
            IsVisible = true,
            MaxPlayers = 4,
        };
        PhotonNetwork.CreateRoom("test", roomOptions);
    }
}