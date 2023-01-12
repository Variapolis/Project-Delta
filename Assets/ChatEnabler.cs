using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class ChatEnabler : MonoBehaviour
{
    [Inject] private ClientPlayerModel _playerModel;
    [SerializeField] private TMP_InputField tmpInputField;

    private void Start()
    {
        tmpInputField.onFocusSelectAll = true;
        tmpInputField.onEndEdit.AsObservable().Subscribe(_ =>
        {
            _playerModel.PlayerObject.Value.GetComponent<PlayerMovementController>().enabled = true;
            tmpInputField.gameObject.SetActive(false);
            tmpInputField.interactable = false;
        }).AddTo(gameObject);
    }


    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.T) || tmpInputField.isFocused) return;
        _playerModel.PlayerObject.Value.GetComponent<PlayerMovementController>().enabled = false;
        tmpInputField.gameObject.SetActive(true);
        tmpInputField.interactable = true;
        tmpInputField.Select();
    }
}