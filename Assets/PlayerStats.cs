using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    void Start() => SaveDataManager.LoadPlayerData();
}