using UnityEngine;

public class PlayerHealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 100f;
    public void Damage(float damage, IDamageable.DamageType damageType = IDamageable.DamageType.Hit)
    {
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

    private void KillPlayer() => Destroy(gameObject);

    private void BlindPlayer()
    {
        throw new System.NotImplementedException();
    }

    private void StunPlayer()
    {
        throw new System.NotImplementedException();
    }
}
