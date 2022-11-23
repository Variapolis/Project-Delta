using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LeaveRoomHandler : MonoBehaviour
{
    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
    }
}
