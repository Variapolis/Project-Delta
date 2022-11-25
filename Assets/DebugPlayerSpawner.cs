using Photon.Pun;
using UnityEngine;
using Zenject;

public class DebugPlayerSpawner : MonoBehaviour
{
    [Inject] private CameraHolderController _cameraHolder;
    [Inject] private CrosshairController _crosshairController;
    [Inject] private DiContainer _diContainer;

    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(Spawn), 5f);
    }

    void Spawn()
    {
        var player = PhotonNetwork.Instantiate("PlayerSpy Variant", Vector3.zero, Quaternion.identity);
        _diContainer.InjectGameObject(player);
        _cameraHolder.target = player.transform;
    }
}