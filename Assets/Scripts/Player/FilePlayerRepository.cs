using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void SavePlayerData(PlayerData data);
    PlayerData LoadPlayerData();
}

public class FilePlayerRepository : IPlayerRepository
{
    private string savePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");

    public void SavePlayerData(PlayerData data)
    {
        string json = data.ToJson();
        File.WriteAllText(savePath, json);
        Debug.Log($"플레이어 데이터 저장: {savePath}");
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return null;
        }

        string json = File.ReadAllText(savePath);
        return PlayerData.FromJson(json);
    }
}
