using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    // 저장 경로 설정
    private static string SavePath => Path.Combine(Application.persistentDataPath, "PlayerData.json");

    // 플레이어 데이터 저장
    public static void SavePlayerData(PlayerData data)
    {
        // JSON으로 직렬화 후 파일 저장
        string json = data.ToJson();
        File.WriteAllText(SavePath, json);
        Debug.Log($"플레이어 데이터가 저장되었습니다: {SavePath}");
    }

    // 플레이어 데이터 불러오기
    public static PlayerData LoadPlayerData()
    {
        string json = File.ReadAllText(SavePath);
        PlayerData data = PlayerData.FromJson(json);
        Debug.Log("플레이어 데이터 불러오기 완료");
        return data;
    }
}
