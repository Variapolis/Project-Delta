using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public sealed class MasterClientButtonController : MonoBehaviour
{
    [SerializeField] private Button button;

    private void OnEnable() => button.gameObject.SetActive(PhotonNetwork.IsMasterClient);
}