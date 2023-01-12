using System;
using System.IO;
using System.Text;
using Leguar.TotalJSON;

public static class SaveDataManager
{
    private const string SaveDataPath = "playerData.json";
    private const string SaveSecret = "InsecureSecret";
    private static PlayerData _loadedPlayerData;
    private static bool _isLoaded = false;

    public static PlayerData LoadPlayerData()
    {
        if (_isLoaded) return _loadedPlayerData;
        if (!File.Exists(SaveDataPath)) SaveNewPlayerData();
        var decrypted = Decrypt(File.ReadAllText(SaveDataPath));
        _isLoaded = true;
        return _loadedPlayerData = JSON.ParseString(decrypted).Deserialize<PlayerData>();
    }

    public static void SavePlayerData(PlayerData data)
    {
        _loadedPlayerData = data;
        File.WriteAllText(SaveDataPath, Encrypt(JSON.Serialize(data).CreateString()));
    }

    private static void SaveNewPlayerData() =>
        File.WriteAllText(SaveDataPath,
            Encrypt(JSON.Serialize(new PlayerData { guid = Guid.NewGuid().ToString() }).CreateString()));

    private static string Encrypt(string value)
    {
        var encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        var encrypted = XORCipher(encoded);
        return encrypted;
    }

    private static string Decrypt(string value)
    {
        var decrypted = XORCipher(value);
        var encoded = Convert.FromBase64String(decrypted);
        return Encoding.ASCII.GetString(encoded);
    }

    private static string XORCipher(string value)
    {
        var result = string.Empty;
        for (int i = 0; i < value.Length; i++) result += (char)(value[i] ^ SaveSecret[i % SaveSecret.Length]);
        return result;
    }
}

public struct PlayerData
{
    public string guid;
    public string username;
    public int kills;
    public int deaths;
    public int kdRatio;
}