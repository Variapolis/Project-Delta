using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [Inject] private PhotonView _photonView;
    [Inject] private ClientPlayerModel _playerModel;
    [SerializeField] private float health = 100f;

    public void Damage(float damage, Player attacker, IDamageable.DamageType damageType = IDamageable.DamageType.Hit)
    {
        if (!_photonView.IsMine) return;
        health -= damage;
        if (health <= 0) KillPlayer(attacker);

        if (damageType == IDamageable.DamageType.Stun) StunPlayer();
        if (damageType == IDamageable.DamageType.Blind) BlindPlayer();
    }

    private void KillPlayer(Player attacker)
    {
        if (!Equals(attacker, _photonView.Owner)) attacker.AddScore(1);
        if (_photonView.IsMine) _playerModel.IsAlive.Value = false;
        PhotonNetwork.Destroy(gameObject);
    }

    private void BlindPlayer()
    {
        throw new System.NotImplementedException();
    }

    private void StunPlayer()
    {
        throw new System.NotImplementedException();
    }
}