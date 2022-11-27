using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public Vector3 worldPosition
    {
        get
        {
            var ray = Camera.main.ScreenPointToRay(transform.position);
            Physics.Raycast(ray, out var hit, 30f);
            return hit.point;
        }
    }

    // private void Start() => Cursor.visible = false;

    void Update() => transform.position = Input.mousePosition;
}
