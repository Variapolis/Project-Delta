using UniRx;
using UnityEngine;
using Zenject;

public class MenuUIController : MonoBehaviour
{
    [Inject] private MainMenuModel _menuModel;

    [SerializeField] private MenuState openState;
    
    private void Awake()
    {
        _menuModel.MenuState
            .DistinctUntilChanged()
            .Where(s => gameObject.activeSelf != (s == openState))
            .Subscribe(s =>
            {
                gameObject.SetActive(s == openState);
                Debug.Log($"{gameObject.name}: {openState} - {s}: {s == openState}");
            })
            .AddTo(gameObject);
    }
}

