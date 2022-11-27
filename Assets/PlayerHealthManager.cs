using Photon.Pun;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private PhotonView photonView;
    [SerializeField] private float health = 100f;
    
    [PunRPC]
    public void Damage(float damage, IDamageable.DamageType damageType = IDamageable.DamageType.Hit)
    {
        if (!photonView.IsMine) return;
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Debug.Log("Killed");
            KillPlayer();
        }

        if (damageType == IDamageable.DamageType.Stun) StunPlayer();
        if (damageType == IDamageable.DamageType.Blind) BlindPlayer();
    }

    private void KillPlayer() => PhotonNetwork.Destroy(gameObject);

    private void BlindPlayer()
    {
        throw new System.NotImplementedException();
    }

    private void StunPlayer()
    {
        throw new System.NotImplementedException();
    }
}
