using Photon.Pun;
using UnityEngine;

public class CloseLobby : MonoBehaviour
{
    public void LeaveLobby()
    {
        if (PhotonNetwork.InLobby) PhotonNetwork.LeaveLobby();
    }
}