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

    public void Initialize()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        // 파일이 존재하지 않으면 빈 데이터 파일을 생성
        if (!File.Exists(filePath))
        {
            Debug.Log($"파일이 존재하지 않음. 경로: {filePath}");
            File.WriteAllText(filePath, JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(new List<PlayerData>())));
            Debug.Log("빈 데이터 파일 생성 완료.");
        }
        else
        {
            Debug.Log($"파일이 존재합니다. 경로: {filePath}");
        }
    }


    // 전체 캐릭터 데이터 저장
    public void SaveAllPlayerData(List<PlayerData> data)
    {
        Debug.Log("PlayerData 저장 시작");
        Debug.Log($"저장할 데이터 수: {data.Count}");
        foreach (var playerData in data)
        {
            Debug.Log($"저장할 플레이어: {playerData.NickName}");
        }

        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(data));
        File.WriteAllText(filePath, json);
        Debug.Log($"PlayerData 저장 완료. 경로: {filePath}");
    }


    // 전체 캐릭터 데이터 불러오기
    public List<PlayerData> LoadAllPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return new List<PlayerData>();
        }

        Debug.Log($"파일 존재 확인 완료. 경로: {filePath}");

        // 데이터 파일에서 읽어서 반환
        var json = File.ReadAllText(filePath);
        Debug.Log($"파일에서 읽은 JSON: {json}");

        var wrapper = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json);
        Debug.Log($"로드된 데이터 수: {wrapper.List.Count}");

        return wrapper.List;
    }

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
