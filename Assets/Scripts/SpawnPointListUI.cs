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
    [Inject] private ClientPlayerModel _playerModel;
    [SerializeField] private GameObject spawnPointUIPrefab;
    [SerializeField] private Transform spawnPointUIContent;

    private void Start()
    {
        var playerTeamCode = PhotonNetwork.LocalPlayer.GetPhotonTeam()?.Code ?? 1;
        foreach (var spawnPoint in _spawnPointList)
        {
            if ((byte)spawnPoint.Team != playerTeamCode) continue;
            var spawnUI = Instantiate(spawnPointUIPrefab, spawnPointUIContent).GetComponent<SpawnPointUI>();
            spawnUI.SpawnName = spawnPoint.LocationName;
            spawnUI.button.onClick.AsObservable().Subscribe(_ =>
            {
                var player = _playerFactory.Create(spawnPoint.transform.position);
                _playerModel.PlayerObject.Value = player;
                _playerModel.IsAlive.Value = true;
                _cameraHolderController.target = player.transform;
            }).AddTo(gameObject);
        }

        _playerModel.IsAlive
            .DistinctUntilChanged()
            .Subscribe(b => gameObject.SetActive(!b))
            .AddTo(gameObject);
    }
}