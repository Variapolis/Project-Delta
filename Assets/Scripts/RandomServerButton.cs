using Photon.Pun;

public class RandomServerButton : MonoBehaviourPunCallbacks
{
    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();
}