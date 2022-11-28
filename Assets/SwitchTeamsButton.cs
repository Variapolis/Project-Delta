using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTeamsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    void Start()
    {
        _button.onClick.AsObservable().Subscribe(_ =>
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LocalPlayer.SwitchTeam(PhotonNetwork.LocalPlayer.GetPhotonTeam().Code == 1 ? (byte)2 : (byte)1);
        }).AddTo(gameObject);
    }
}
