using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ReturnToMenuButton : MonoBehaviour
{
    [Inject] private MainMenuModel _menuModel;
    [SerializeField] private Button button;

    private void Reset() => button = GetComponent<Button>();

    void Start() => button.onClick.AsObservable().Subscribe(_ => _menuModel.SetMenuState(MenuState.MainMenu)).AddTo(gameObject);
}
