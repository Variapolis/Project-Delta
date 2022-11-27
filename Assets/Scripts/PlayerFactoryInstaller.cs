using JetBrains.Annotations;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using Zenject;

public sealed class PlayerFactoryInstaller : MonoInstaller
{
    [SerializeField] private GameObject spyPrefab;
    [SerializeField] private GameObject mercPrefab;

    public override void InstallBindings() => Container.BindFactory<Vector3, GameObject, PlayerFactory>()
        .FromIFactory(x => x.To<ClientOnlyFactory>().AsSingle().WithArguments(spyPrefab.name, mercPrefab.name));

    [UsedImplicitly]
    private sealed class ClientOnlyFactory : IFactory<Vector3, GameObject>
    {
        private readonly string _spyPrefabName;
        private readonly string _mercPrefabName;
        private readonly DiContainer _diContainer;

        public ClientOnlyFactory(string spyPrefabName, string mercPrefabName, DiContainer diContainer)
        {
            _spyPrefabName = spyPrefabName;
            _mercPrefabName = mercPrefabName;
            _diContainer = diContainer;
        }

        public GameObject Create(Vector3 spawnPoint)
        {
            var player = PhotonNetwork.Instantiate(
                PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1 ? _spyPrefabName : _mercPrefabName, spawnPoint,
                Quaternion.identity);
            _diContainer.InjectGameObject(player);
            return player;
        }
    }
}