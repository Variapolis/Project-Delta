using TMPro;
using UnityEngine;

public class AimingTestDebug : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = (Input.GetKey(KeyCode.Mouse1) ? "Aiming" : "Not Aiming") + " (";
        if (Input.GetKey(KeyCode.W)) text.text += "W";
        if (Input.GetKey(KeyCode.A)) text.text += "A";
        if (Input.GetKey(KeyCode.S)) text.text += "S";
        if (Input.GetKey(KeyCode.D)) text.text += "D";
        text.text += ")";
    }
}