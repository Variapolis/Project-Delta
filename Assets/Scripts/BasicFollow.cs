using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    [SerializeField] private Transform parent;
    private Vector3 _offset;

    private void Start() => _offset = transform.position - parent.position;

    private void Update() => transform.position = parent.transform.position + _offset;
}
