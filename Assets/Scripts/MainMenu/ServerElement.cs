using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ServerElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text map;
    [SerializeField] private TMP_Text players;
    [SerializeField] private TMP_Text latency;

    private RoomInfo _room;

    public RoomInfo Room
    {
        get => Room;
        set
        {
            _room = value;
            title.text = value.Name;
            map.text = "Default";
            players.text = $"{value.PlayerCount.ToString()}/{value.MaxPlayers.ToString()}";
            latency.text = PhotonNetwork.GetPing().ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData) => PhotonNetwork.JoinRoom(_room.Name);
}