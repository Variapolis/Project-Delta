using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCursor : MonoBehaviour
{
    [SerializeField] private CrosshairController crosshair;

    private void Update() => transform.position = crosshair.worldPosition;
}
