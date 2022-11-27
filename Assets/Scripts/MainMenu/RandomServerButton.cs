using System;
using Photon.Pun;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RandomServerButton : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button _button;
    private void Reset() => _button = GetComponent<Button>();

    private void Start() => _button.onClick.AsObservable().Subscribe(_ => PhotonNetwork.JoinRandomRoom()).AddTo(gameObject);
}