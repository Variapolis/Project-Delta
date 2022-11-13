using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Zenject;

public class ServerElement : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text map;
    [SerializeField] private TMP_Text players;
    [SerializeField] private TMP_Text latency;

    private RoomInfo _room;

    [Inject]
    private void Construct(RoomInfo room)
    {
        _room = room;
        title.text = room.Name;
        map.text = "Default";
        players.text = $"{room.PlayerCount.ToString()}/{room.MaxPlayers.ToString()}";
        latency.text = PhotonNetwork.GetPing().ToString();
    }
    [UsedImplicitly]
    public class Factory : PlaceholderFactory<RoomInfo, ServerElement>
    {
    }
}