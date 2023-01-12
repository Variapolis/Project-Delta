using UnityEngine;

public class StopCoilWhine : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
    }
}