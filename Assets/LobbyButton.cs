using Photon.Pun;
using UnityEngine;

public class LobbyButton : MonoBehaviour
{
    public void JoinLobby() => PhotonNetwork.JoinLobby();
}