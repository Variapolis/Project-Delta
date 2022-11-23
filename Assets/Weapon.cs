using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ParticleSystem bulletOrigin;
    [SerializeField] private Transform leftHandIKTarget;
    [SerializeField] private Transform leftHandIKHint;

    public Transform LeftHandIKTarget => leftHandIKTarget;
    public Transform LeftHandIKHint => leftHandIKHint;
    public void Fire() => bulletOrigin.Play();
}
