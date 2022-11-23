using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCursor : MonoBehaviour
{
    public CrosshairController crosshair;

    private void Update()
    {
        if (!crosshair) return;
        transform.position = crosshair.worldPosition;
    }
}
