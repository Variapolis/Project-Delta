using System.IO;
using Leguar.TotalJSON;

public static class SaveDataManager
{
    public const string SaveDataPath = "playerData.json";
    public static bool TryLoadPlayerData(out PlayerData data)
    {
        data = default;
        if (!File.Exists(SaveDataPath))
        {
            SavePlayerData(new PlayerData());
            return false;
        }
        JSON.ParseString(File.ReadAllText(SaveDataPath)).Deserialize<PlayerData>();
        return true;
    }

    public static void SavePlayerData(PlayerData data) => File.WriteAllText(SaveDataPath, JSON.Serialize(data).CreateString());
}

public struct PlayerData
{
    public string username;
    public int kills;
    public int deaths;
    public int kdRatio;
}
