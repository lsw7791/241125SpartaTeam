using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UGS.Editor.GoogleDriveExplorerGUI;

public interface IPlayerRepository
{
    void SavePlayerData(List<PlayerData> data);  // List<PlayerData> 받도록 수정
    List<PlayerData> LoadPlayerData();  // List<PlayerData> 반환하도록 수정
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;  // 파일 경로만 사용

    // 생성자에서 persistentDataPath를 사용하지 않음
    public FilePlayerRepository()
    {
        // 생성자에서는 아무 것도 하지 않음
    }

    // Initialize() 메서드에서 persistentDataPath 사용
    public void Initialize()
    {
        // Application.persistentDataPath를 사용할 때는 Awake나 Start에서 호출해야 합니다.
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
