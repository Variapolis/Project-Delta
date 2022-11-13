using Photon.Realtime;
using UnityEngine;
using Zenject;

public class ServerListInstaller : MonoInstaller
{
    public GameObject serverElementPrefab;

    public override void InstallBindings() => Container.BindFactory<RoomInfo, ServerElement, ServerElement.Factory>();
}