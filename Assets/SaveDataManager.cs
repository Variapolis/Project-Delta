using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveDataManager
{
    public static PlayerData LoadPlayerData()
    {
        return new PlayerData();
    }

    public static void SavePlayerData(PlayerData data)
    {
        
    }
}

public struct PlayerData
{
    public string username;
    public int kills;
    public int deaths;
    public int kdRatio;
}
