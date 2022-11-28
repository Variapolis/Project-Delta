using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public sealed class MasterClientButtonController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button button;

    public override void OnEnable()
    {
        button.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        base.OnEnable();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (PhotonNetwork.IsMasterClient) button.gameObject.SetActive(true);
    }
}