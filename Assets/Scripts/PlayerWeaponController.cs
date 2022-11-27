using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class PlayerWeaponController : MonoBehaviour
{
    [Inject] private PhotonView _photonView;
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private List<Weapon> weapons; // TODO: Add weapon selection

    public void FireWeapon() => _photonView.RPC(nameof(RPCFireWeapon), RpcTarget.AllViaServer, _photonView.Owner);

    [PunRPC]
    private void RPCFireWeapon(Player owner) => equippedWeapon.Fire(owner);
}
