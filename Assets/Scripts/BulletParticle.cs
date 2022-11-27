using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private int _damage;
    private Player _owner;

    private List<ParticleCollisionEvent> _collisionEvents;

    private void Reset() => _particleSystem = GetComponent<ParticleSystem>();

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var hit)) Hit(hit);
    }

    public void Fire(Player owner, int damage)
    {
        _owner = owner;
        _damage = damage;
        _particleSystem.Play();
    }

    private void Hit(IDamageable hit) => hit.Damage(_damage, _owner);
}