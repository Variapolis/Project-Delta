using UnityEngine;
using Zenject;

public class TrackCursor : MonoBehaviour
{
    [Inject] CrosshairController crosshair;

    private void Update()
    {
        if (!crosshair) return;
        transform.position = crosshair.worldPosition;
    }
}
