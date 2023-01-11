using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class LeaderboardButton : MonoBehaviour
{
    [Inject] private MainMenuModel _menuModel;
    [SerializeField] private Button _button;
    private void Reset() => _button = GetComponent<Button>();

    private void Start() => _button.onClick.AsObservable().Subscribe(_ => _menuModel.SetMenuState(MenuState.Leaderboard)).AddTo(gameObject);
}
