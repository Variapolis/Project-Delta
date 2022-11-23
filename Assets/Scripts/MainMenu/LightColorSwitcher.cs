using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorSwitcher : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    [SerializeField] private float timeBeforeChange;
    private float _time;

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time < timeBeforeChange) return;
        _time = 0f;
        SwitchColor();
    }

    private void SwitchColor() => _light.color = _light.color == _color1 ? _color2 : _color1;
}