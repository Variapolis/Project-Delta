using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [Inject] private PhotonView _photonView;
    [Inject] private ClientPlayerModel _playerModel;
    // [SerializeField] private ReactiveProperty<float> health = new(100);
    [SerializeField] private float startingHealth = 100f;
    private float Health
    {
        get => _playerModel.PlayerHealth.Value;
        set => _playerModel.PlayerHealth.Value = value;
    }

    private void Start() => Health = startingHealth;

    public void Damage(float damage, Player attacker, IDamageable.DamageType damageType = IDamageable.DamageType.Hit)
    {
        if (!_photonView.IsMine) return;
        Health -= damage;
        if (Health <= 0) KillPlayer(attacker);

        if (damageType == IDamageable.DamageType.Stun) StunPlayer();
        if (damageType == IDamageable.DamageType.Blind) BlindPlayer();
    }

    private void KillPlayer(Player attacker)
    {
        if (!Equals(attacker.GetPhotonTeam(), _photonView.Owner.GetPhotonTeam())) attacker.AddScore(1);
        if (_photonView.IsMine) _playerModel.IsAlive.Value = false;
        Health = 0;
        PhotonNetwork.Destroy(gameObject);
    }

    private void BlindPlayer() => throw new NotImplementedException();

    private void StunPlayer() => throw new NotImplementedException();
}