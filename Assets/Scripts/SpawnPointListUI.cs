using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UniRx;
using UnityEngine;
using Zenject;

public class SpawnPointListUI : MonoBehaviour
{
    [Inject] private SpawnPointList _spawnPointList;
    [Inject] private PlayerFactory _playerFactory;
    [Inject] private CameraHolderController _cameraHolderController;
    [SerializeField] private GameObject spawnPointUIPrefab;
    [SerializeField] private Transform spawnPointUIContent;

    private void Start()
    {
        var playerTeamCode = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code;
        foreach (var spawnPoint in _spawnPointList)
        {
            if ((byte)spawnPoint.Team != playerTeamCode) continue;
            var spawnUI = Instantiate(spawnPointUIPrefab, spawnPointUIContent).GetComponent<SpawnPointUI>();
            spawnUI.SpawnName = spawnPoint.LocationName;
            spawnUI.button.onClick.AsObservable().Subscribe(_ =>
            {
                var player = _playerFactory.Create(spawnPoint.transform.position);
                _cameraHolderController.target = player.transform;
            }).AddTo(gameObject);
        }
    }
}