using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

public class WeaponIKHandler : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private TwoBoneIKConstraint leftHandConstraint;
    [SerializeField] private List<MultiAimConstraint> ikAimConstraints;
    [Inject] private PhotonView _photonView;

    public void SwitchWeapon(Weapon weapon)
    {
        leftHandConstraint.data.hint = weapon.LeftHandIKHint;
        leftHandConstraint.data.target = weapon.LeftHandIKTarget;
    }

    public void Aim() => _photonView.RPC(nameof(RPCAim), RpcTarget.AllViaServer);
    public void StopAiming() => _photonView.RPC(nameof(RPCStopAim), RpcTarget.AllViaServer);

    [PunRPC]
    private void RPCAim() => rig.weight = Mathf.Lerp(rig.weight, 1, Time.deltaTime * speed);

    [PunRPC]
    private void RPCStopAim() => rig.weight = Mathf.Lerp(rig.weight, 0, Time.deltaTime * speed);
}
