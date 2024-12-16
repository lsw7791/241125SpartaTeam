using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void SavePlayerData(List<PlayerData> data);  // 데이터를 저장하는 메서드
    List<PlayerData> LoadPlayerData();  // 데이터를 불러오는 메서드
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    public FilePlayerRepository()
    {
        // 생성자에서 초기화는 하지 않음
        Debug.Log("FilePlayerRepository 생성자 호출");
    }

    // Initialize 메서드 추가
    public void Initialize()
    {
        Debug.Log("FilePlayerRepository Initialize 호출");
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");  // 파일이 없으면 빈 JSON 배열로 초기화
        }
    }

    public void SavePlayerData(List<PlayerData> data)
    {
        string json = JsonUtility.ToJson(new PlayerDataListWrapper { playerDataList = data });
        File.WriteAllText(filePath, json);  // savePath 대신 filePath 사용
        Debug.Log($"플레이어 데이터 저장: {filePath}");
    }

    public List<PlayerData> LoadPlayerData()
    {
        if (!File.Exists(filePath))  // savePath 대신 filePath 사용
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return new List<PlayerData>(); // 데이터가 없으면 빈 리스트 반환
        }

        string json = File.ReadAllText(filePath);  // savePath 대신 filePath 사용
        return JsonUtility.FromJson<PlayerDataListWrapper>(json).playerDataList;
    }
}


[System.Serializable]
public class PlayerDataListWrapper
{
    public List<PlayerData> playerDataList;
}
