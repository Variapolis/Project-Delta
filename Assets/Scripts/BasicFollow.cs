using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicFollow : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private float speed;
    private Vector3 _offset;

    private void Start() => _offset = transform.position - parent.position;

    private void Update() => transform.position = Vector3.Slerp(transform.position, parent.transform.position + _offset,
        Time.deltaTime * speed * (transform.position - (transform.position + _offset)).magnitude);
}