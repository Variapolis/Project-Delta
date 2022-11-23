using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiplayerDebugger : MonoBehaviourPunCallbacks
{
    public override void OnCreatedRoom() => Debug.Log("Room Created");

    public override void OnJoinedLobby() => Debug.Log("Joined Lobby");

    public override void OnLeftLobby() => Debug.Log("Left Lobby");

    public override void OnDisconnected(DisconnectCause cause) => Debug.Log("Disconnected");

    public override void OnJoinedRoom() => Debug.Log("Joined Room");
    public override void OnPlayerEnteredRoom(Player newPlayer) => Debug.Log("Player Joined Room");

    public override void OnPlayerLeftRoom(Player otherPlayer) => Debug.Log("Player Left Room");
}
