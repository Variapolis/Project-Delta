using System;
using System.IO;
using Leguar.TotalJSON;

public static class SaveDataManager
{
    public const string SaveDataPath = "playerData.json";
    public static PlayerData LoadedPlayerData { get; private set; }
    public static bool TryLoadPlayerData(out PlayerData data)
    {
        data = default;
        if (!File.Exists(SaveDataPath))
        {
            SaveNewPlayerData();
            return false;
        }
        data = LoadedPlayerData = JSON.ParseString(File.ReadAllText(SaveDataPath)).Deserialize<PlayerData>();
        return true;
    }

    public static void SavePlayerData(PlayerData data) => File.WriteAllText(SaveDataPath, JSON.Serialize(data).CreateString());

    private static void SaveNewPlayerData() => File.WriteAllText(SaveDataPath, JSON.Serialize(new PlayerData {guid = Guid.NewGuid().ToString()}).CreateString());
}

public struct PlayerData
{
    public string guid;
    public string username;
    public int kills;
    public int deaths;
    public int kdRatio;
}
