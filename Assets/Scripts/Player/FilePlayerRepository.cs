using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void SavePlayerData(List<PlayerData> data);  // List<PlayerData> 받도록 수정
    List<PlayerData> LoadPlayerData();  // List<PlayerData> 반환하도록 수정
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    // 생성자에서 persistentDataPath를 사용하지 않음
    public FilePlayerRepository()
    {
        Debug.Log("FilePlayerRepository 생성자 호출");  // 생성자 호출 로그
    }

    // Initialize() 메서드에서 persistentDataPath 사용
    public void Initialize()
    {
        Debug.Log("FilePlayerRepository Initialize 호출");  // Initialize 호출 로그
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

// PlayerData 리스트를 래핑하는 클래스 추가 (JsonUtility에서 리스트를 직렬화할 수 있도록)
[System.Serializable]
public class PlayerDataListWrapper
{
    public List<PlayerData> playerDataList;
}
