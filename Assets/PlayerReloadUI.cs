using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerReloadUI : MonoBehaviour
{
    [Inject] private ClientPlayerModel _playerModel;
    [SerializeField] private Image ammoImage;

    private void Start()
    {
        _playerModel.IsReloading.Subscribe(b => ammoImage.enabled = b).AddTo(gameObject);
        _playerModel.ReloadStatus.Subscribe(f => ammoImage.fillAmount = f).AddTo(gameObject);
    }
}
