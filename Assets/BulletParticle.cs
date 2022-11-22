using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private List<ParticleCollisionEvent> _collisionEvents;

    private void Reset() => _particleSystem = GetComponent<ParticleSystem>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) _particleSystem.Play();
    }

    private void OnParticleCollision(GameObject other)
    {
        // var events = _particleSystem.GetCollisionEvents(other, _collisionEvents);

        if(other.TryGetComponent<IDamageable>(out var hit)) hit.Damage(100f);
    }
}