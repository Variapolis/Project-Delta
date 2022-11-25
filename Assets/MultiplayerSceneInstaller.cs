using UnityEngine;
using Zenject;

public class MultiplayerSceneInstaller : MonoInstaller
{
    [SerializeField] private CameraHolderController cameraHolder;
    [SerializeField] private CrosshairController crosshair;
    
    public override void InstallBindings()
    {
        Container.Bind<CameraHolderController>().FromInstance(cameraHolder).AsSingle().NonLazy();
        Container.Bind<CrosshairController>().FromInstance(crosshair).AsSingle().NonLazy();
        
    }
}