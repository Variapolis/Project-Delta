using Photon.Pun;
using UnityEngine;

public class LoadOnlinePlay : MonoBehaviour
{
    void Start()
    {
        if (PhotonNetwork.IsMasterClient) Invoke(nameof(LoadScene), 1);
    }

    private void LoadScene() => PhotonNetwork.LoadLevel("OnlinePlay");
}