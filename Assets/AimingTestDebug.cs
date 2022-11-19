using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AimingTestDebug : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    // Update is called once per frame
    void Update() => text.text = Input.GetKey(KeyCode.Mouse1) ? "Aiming" : "Not Aiming";
}