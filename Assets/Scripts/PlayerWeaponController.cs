using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class PlayerWeaponController : MonoBehaviour
{
    [Inject] private PhotonView _photonView;
    [Inject] private ClientPlayerModel _playerModel;
    [SerializeField] private Weapon equippedWeapon;
    [SerializeField] private List<Weapon> weapons; // TODO: Add weapon selection
    [SerializeField] private AudioSource reloadAudio;
    public Weapon.FireRateType WeaponFireRate => equippedWeapon.FireRate;

    public void FireWeapon()
    {
        if (_playerModel.IsReloading.Value || equippedWeapon.AmmoInMag == 0) return;
        _photonView.RPC(nameof(RPCFireWeapon), RpcTarget.AllViaServer, _photonView.Owner);
    }

    [PunRPC]
    private void RPCFireWeapon(Player owner) => equippedWeapon.Fire(owner);

    public void Reload()
    {
        if (!_playerModel.IsReloading.Value) StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        reloadAudio.Play();
        _playerModel.IsReloading.Value = true;
        _playerModel.ReloadStatus.Value = 0;
        while (_playerModel.IsReloading.Value)
        {
            yield return new WaitForEndOfFrame();
            _playerModel.ReloadStatus.Value += Time.deltaTime * equippedWeapon.ReloadSpeed;
            if (_playerModel.ReloadStatus.Value > 1) _playerModel.IsReloading.Value = false;
        }
        equippedWeapon.Reload();
    }
}