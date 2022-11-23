using Photon.Pun;
using Photon.Realtime;
using Zenject;

public class MainMenuController : MonoBehaviourPunCallbacks
{
    [Inject] private MainMenuModel _menuModel;
    
    // public override void OnConnected() => _menuModel.SetMenuState(MenuState.MainMenu);

    public override void OnConnectedToMaster() => _menuModel.SetMenuState(MenuState.MainMenu);

    public override void OnLeftRoom() => _menuModel.SetMenuState(MenuState.MainMenu);

    public override void OnCreatedRoom() => _menuModel.SetMenuState(MenuState.Room);

    public override void OnJoinedRoom() => _menuModel.SetMenuState(MenuState.Room);

    public override void OnJoinedLobby() => _menuModel.SetMenuState(MenuState.Lobby);

    public override void OnLeftLobby() => _menuModel.SetMenuState(MenuState.MainMenu);

    public override void OnDisconnected(DisconnectCause cause) => _menuModel.SetMenuState(MenuState.Disconnected);
}