using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class TrainingTargetController : MonoBehaviour, IDamageable
{
    [SerializeField] private AudioSource hitSound;
    public void Damage(float damage, Player attacker = null, IDamageable.DamageType damageType = IDamageable.DamageType.Hit) => hitSound.Play();
}
