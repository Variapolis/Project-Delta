using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private List<ParticleCollisionEvent> _collisionEvents;

    private void Reset() => _particleSystem = GetComponent<ParticleSystem>();

    private void OnParticleCollision(GameObject other)
    {
        if(other.TryGetComponent<IDamageable>(out var hit)) Hit(hit);
    }

    [PunRPC]
    private void Hit(IDamageable hit) => hit.Damage(100f);
}