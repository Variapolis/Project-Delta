using Photon.Pun;
using UnityEngine;

public class DebugPlayerSpawner : MonoBehaviour
{
    [SerializeField] private CrosshairController crosshairController;
    [SerializeField] private BasicFollow cameraHolder;


    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(Spawn), 5f);
    }

    void Spawn()
    {
        var player = PhotonNetwork.Instantiate("PlayerSpy Variant", Vector3.zero, Quaternion.identity).GetComponent<PlayerMovementController>();
        cameraHolder.parent = player.transform;
        player.cameraHolder = cameraHolder.transform;
        player.crosshairController = crosshairController;
        player.GetComponentInChildren<TrackCursor>().crosshair = crosshairController;
    }
}