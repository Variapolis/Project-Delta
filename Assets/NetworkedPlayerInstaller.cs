using Photon.Pun;
using UniRx;
using UnityEngine;
using Zenject;

public class NetworkedPlayerInstaller : MonoInstaller
{
    [SerializeField] private PhotonView _photonView;
    
    public override void InstallBindings()
    {
        Container.Bind<ReactiveProperty<Weapon>>().AsSingle().NonLazy();
        Container.Bind<PhotonView>().FromInstance(_photonView).AsSingle().NonLazy();
    }
}
