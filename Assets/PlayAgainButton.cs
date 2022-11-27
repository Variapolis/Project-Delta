using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;

    private void Reset() => playAgainButton = GetComponent<Button>();

    void Start() => playAgainButton.onClick
        .AsObservable()
        .Subscribe(_ =>
        {
            foreach (var player in PhotonNetwork.PlayerList) player.SetScore(0);
            PhotonNetwork.LoadLevel("ReloadingOnline");
        })
        .AddTo(gameObject);
}