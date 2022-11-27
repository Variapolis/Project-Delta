using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public sealed class StartButtonController : MonoBehaviour
{
    [SerializeField] private Button startGameButton;

    private void OnEnable() => startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
}