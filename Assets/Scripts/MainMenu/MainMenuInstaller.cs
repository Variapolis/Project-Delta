using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private MainMenuController _mainMenuController;

    public override void InstallBindings()
    {
        Container.Bind<MainMenuModel>().AsSingle().NonLazy();
        Container.BindInstance(_mainMenuController).AsSingle().NonLazy();
        Container.Bind<IInitializable>().To<MenuUIController>().FromComponentsInHierarchy().AsCached();
    }
}