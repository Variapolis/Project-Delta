using JetBrains.Annotations;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public sealed class PlayerFactoryInstaller : MonoInstaller
{
    [SerializeField] private GameObject spyPrefab;
    [SerializeField] private GameObject mercPrefab;

    public override void InstallBindings() => Container.BindFactory<GameObject, PlayerFactory>()
        .FromIFactory(x => x.To<ClientOnlyFactory>().AsSingle().WithArguments(spyPrefab.name, mercPrefab.name));

    [UsedImplicitly]
    private sealed class ClientOnlyFactory : IFactory<GameObject>
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

        public GameObject Create()
        {
            var player = PhotonNetwork.Instantiate(true ? _spyPrefabName : _mercPrefabName, Vector3.zero,
                Quaternion.identity);
            _diContainer.InjectGameObject(player);
            return player;
        }
    }
}