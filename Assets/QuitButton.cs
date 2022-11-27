using Photon.Pun;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private Button quitButton;

    private void Reset() => quitButton = GetComponent<Button>();

    void Start() => quitButton.onClick
        .AsObservable()
        .Subscribe(_ => PhotonNetwork.LeaveRoom())
        .AddTo(gameObject);
}
