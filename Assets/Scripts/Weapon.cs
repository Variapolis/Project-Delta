using System;
using System.Collections;
using Photon.Realtime;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private BulletParticle bulletOrigin;
    [SerializeField] private Transform leftHandIKTarget;
    [SerializeField] private Transform leftHandIKHint;
    [SerializeField] private int damage;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int magazineSize;
    [SerializeField] private FireRateType fireRate;
    [SerializeField] private float timeToFire;
    [SerializeField] private AudioSource gunshotSource;
    [SerializeField] private float reloadSpeed;

    private bool _readyToFire;
    private int _spareAmmo;

    public int AmmoInMag { get; private set; }

    public FireRateType FireRate => fireRate;
    public float ReloadSpeed => reloadSpeed;

    public Transform LeftHandIKTarget => leftHandIKTarget;
    public Transform LeftHandIKHint => leftHandIKHint;

    private void Start()
    {
        _readyToFire = true;
        _spareAmmo = maxAmmo;
        AmmoInMag = magazineSize;
    }

    public void Fire(Player owner)
    {
        if (owner.IsLocal && (AmmoInMag == 0 || !_readyToFire)) return;
        AmmoInMag--;
        StartCoroutine(CooldownTimer());
        bulletOrigin.Fire(owner, damage);
        gunshotSource.Play();
    }

    private IEnumerator CooldownTimer()
    {
        _readyToFire = false;
        var elapsedTime = timeToFire;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime -= Time.deltaTime;
            if (!(elapsedTime <= 0f)) continue;
            _readyToFire = true;
            break;
        }
    }

    public void Reload()
    {
        if (_spareAmmo <= 0) return;
        AmmoInMag = magazineSize;
        // Math.Max(_spareAmmo - (_spareAmmo - magazineSize), 0);
        // TODO: Remove spare ammo after reload
    }

    public enum FireRateType
    {
        Single,
        Auto
    }
}