using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void SaveAllPlayerData(List<PlayerData> data); // 전체 데이터 저장
    List<PlayerData> LoadAllPlayerData();          // 전체 데이터 불러오기
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    // Initialize 메서드 추가
    public void Initialize()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        if (!File.Exists(filePath))
        {
            // 파일이 존재하지 않으면 빈 JSON 배열을 저장
            File.WriteAllText(filePath, JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(new List<PlayerData>())));
        }
    }

    public void SaveAllPlayerData(List<PlayerData> data)
    {
        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(data));
        File.WriteAllText(filePath, json);
        Debug.Log("PlayerData 저장 완료");
    }

    public List<PlayerData> LoadAllPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return new List<PlayerData>();
        }

        var json = File.ReadAllText(filePath);
        var wrapper = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json);
        Debug.Log("PlayerData 로드 완료");
        return wrapper.List;
    }

    [System.Serializable]
    public class SerializableListWrapper<T>
    {
        public List<T> List;

        public SerializableListWrapper(List<T> list)
        {
            List = list;
        }
    }
}

