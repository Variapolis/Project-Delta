using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class WeaponIKHandler : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private TwoBoneIKConstraint leftHandConstraint;
    [SerializeField] private List<MultiAimConstraint> ikAimConstraints;

    public void SwitchWeapon(Weapon weapon)
    {
        leftHandConstraint.data.hint = weapon.LeftHandIKHint;
        leftHandConstraint.data.target = weapon.LeftHandIKTarget;
    }
    
    public void Aim() => rig.weight = Mathf.Lerp(rig.weight, 1, Time.deltaTime * speed);
    public void StopAiming() => rig.weight = Mathf.Lerp(rig.weight, 0, Time.deltaTime * speed);
}
