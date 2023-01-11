using UnityEngine;

public class StopCoilWhine : MonoBehaviour
{
#if UNITY_EDITOR
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
#endif
}