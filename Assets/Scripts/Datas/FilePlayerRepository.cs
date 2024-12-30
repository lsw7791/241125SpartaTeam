using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void Initialize();                  // 초기화
    void SaveCurrentPlayer(PlayerData data); // 현재 플레이 캐릭터 저장
    PlayerData LoadCurrentPlayer();    // 현재 플레이 캐릭터 로드
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    public void Initialize()
    {
        filePath = Path.Combine(Application.persistentDataPath, "CurrentPlayerData.json");

        if (!File.Exists(filePath))
        {
            Debug.Log($"파일이 존재하지 않음. 경로: {filePath}");
            File.WriteAllText(filePath, string.Empty); // 빈 파일 생성
        }
        else
        {
            Debug.Log($"파일이 존재합니다. 경로: {filePath}");
        }
    }

    public void SaveCurrentPlayer(PlayerData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log($"현재 플레이 캐릭터 저장 완료. 경로: {filePath}");
    }

    public PlayerData LoadCurrentPlayer()
    {
        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(File.ReadAllText(filePath)))
        {
            Debug.LogWarning("저장된 현재 플레이 캐릭터 데이터가 없습니다.");
            return null;
        }

        var json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
