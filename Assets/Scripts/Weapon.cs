using System.Collections;
using Photon.Realtime;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private BulletParticle bulletOrigin;
    [SerializeField] private Transform leftHandIKTarget;
    [SerializeField] private Transform leftHandIKHint;
    [SerializeField] private int maxAmmo;
    [SerializeField] private int magazineSize;
    [SerializeField] private FireRateType fireRate;
    [SerializeField] private float timeToFire;

    private bool readyToFire;
    private int _spareAmmo;
    private int _ammoInMag;
    public FireRateType FireRate => fireRate;

    public Transform LeftHandIKTarget => leftHandIKTarget;
    public Transform LeftHandIKHint => leftHandIKHint;

    private void Start()
    {
        readyToFire = true;
        _spareAmmo = maxAmmo;
        _ammoInMag = magazineSize;
    }

    public void Fire(Player owner)
    {
        if (_ammoInMag == 0 || !readyToFire) return;
        _ammoInMag--;
        StartCoroutine(CooldownTimer());
        bulletOrigin.Fire(owner);
    }

    private IEnumerator CooldownTimer()
    {
        readyToFire = false;
        var elapsedTime = timeToFire;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            elapsedTime -= Time.deltaTime;
            if (!(elapsedTime <= 0f)) continue;
            readyToFire = true;
            break;
        }
    }

    public enum FireRateType
    {
        Single,
        Auto
    }
}