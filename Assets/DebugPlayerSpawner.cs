using UnityEngine;
using Zenject;

public class DebugPlayerSpawner : MonoBehaviour
{
    [Inject] private PlayerFactory _playerFactory;
    [Inject] private CameraHolderController _cameraHolder;

    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(Spawn), 1f);
    }

    void Spawn()
    {
        var player = _playerFactory.Create();
        _cameraHolder.target = player.transform;
    }
}