using System;
using UnityEngine;

public class TopDownCameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private void Update() => transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("RotateCamera") * rotationSpeed);
}