using Photon.Pun;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayAgainButton : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;

    private void Reset() => playAgainButton = GetComponent<Button>();

    void Start() => playAgainButton.onClick
        .AsObservable()
        .Subscribe(_ => PhotonNetwork.LoadLevel("ReloadingOnline"))
        .AddTo(gameObject);
}