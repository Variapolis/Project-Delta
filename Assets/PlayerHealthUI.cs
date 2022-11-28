using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealthUI : MonoBehaviour
{
    [Inject] private ClientPlayerModel _playerModel;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthImage; // TODO: Colour changes based on health value

    private void Start()
    {
        healthText.text = "";
        _playerModel.PlayerHealth.Subscribe(h => healthText.text = Mathf.CeilToInt(h).ToString());
    }
}