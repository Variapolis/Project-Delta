using Photon.Pun;
using UnityEngine;

public class PlayerClientInitializer : MonoBehaviour
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private PlayerMovementController playerMovementController;
    
    void Start() => playerMovementController.enabled = photonView.IsMine;
}
