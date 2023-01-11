using UniRx;
using UnityEngine;
using Zenject;

public class MenuUIController : MonoBehaviour, IInitializable
{
    [Inject] private MainMenuModel _menuModel;

    [SerializeField] private MenuState openState;

    void IInitializable.Initialize() =>
        _menuModel.MenuState
            .DistinctUntilChanged()
            .Where(s => gameObject.activeSelf != (s == openState))
            .Subscribe(s => gameObject.SetActive(s == openState))
            .AddTo(gameObject);
}

