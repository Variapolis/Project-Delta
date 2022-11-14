using JetBrains.Annotations;
using UniRx;

[UsedImplicitly]
public class MainMenuModel
{
    [UsedImplicitly]
    private readonly ReactiveProperty<MenuState> menuState = new ReactiveProperty<MenuState>();

    public IReadOnlyReactiveProperty<MenuState> MenuState => menuState;

    public void SetMenuState(MenuState state) => menuState.Value = state;
}