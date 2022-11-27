using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SpawnPointUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    public Button button;
    
    public string SpawnName
    {
        get => nameText.text;
        set => nameText.text = value;
    }
}