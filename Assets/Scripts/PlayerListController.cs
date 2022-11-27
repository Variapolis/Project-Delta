using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using UnityEngine;

public class PlayerListController : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform spiesListContent;
    [SerializeField] private Transform guardsListContent;
    [SerializeField] private GameObject playerUIPrefab;


    private void Start()
    {
        PhotonTeamsManager.PlayerJoinedTeam += OnTeamsChanged;
        PhotonTeamsManager.PlayerLeftTeam += OnTeamsChanged;
    }

    public override void OnEnable()
    {
        if (PhotonNetwork.InRoom) RefreshPlayerList();
        base.OnEnable();
    }

    public override void OnJoinedRoom() => RefreshPlayerList();

    public override void OnPlayerEnteredRoom(Player newPlayer) => RefreshPlayerList();

    private void OnTeamsChanged(Player player, PhotonTeam team) => RefreshPlayerList();

    private void RefreshPlayerList()
    {
        spiesListContent.DestroyChildren();
        guardsListContent.DestroyChildren();
        PhotonTeamsManager.Instance.TryGetTeamMembers(1, out var spies);
        foreach (var player in spies)
            Instantiate(playerUIPrefab, spiesListContent).GetComponent<PlayerUIElement>().Player = player;
        PhotonTeamsManager.Instance.TryGetTeamMembers(2, out var guards);
        foreach (var player in guards)
            Instantiate(playerUIPrefab, guardsListContent).GetComponent<PlayerUIElement>().Player = player;
    }

    private void OnDestroy()
    {
        PhotonTeamsManager.PlayerJoinedTeam -= OnTeamsChanged;
        PhotonTeamsManager.PlayerLeftTeam -= OnTeamsChanged;
    }
}   