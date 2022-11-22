using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AimingIKScript : MonoBehaviour
{
    [SerializeField] private Rig _rig;
    [SerializeField] private float speed;

    void Update() => _rig.weight = Mathf.Lerp(_rig.weight, Input.GetMouseButton(1) ? 1 : 0, Time.deltaTime * speed);
}
