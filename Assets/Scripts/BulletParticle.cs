using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

public class BulletParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private Player _owner;

    private List<ParticleCollisionEvent> _collisionEvents;

    private void Reset() => _particleSystem = GetComponent<ParticleSystem>();

    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent<IDamageable>(out var hit)) Hit(hit);
    }

    public void Fire(Player owner)
    {
        _owner = owner;
        _particleSystem.Play();
    }

    private void Hit(IDamageable hit) => hit.Damage(100f, _owner);
}