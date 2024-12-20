using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void Initialize();             // Initialize 메서드 추가
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
        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(data));
        File.WriteAllText(filePath, json);
        Debug.Log($"PlayerData 저장 완료. 경로: {filePath}");

        // PlayerPrefs에 마지막 캐릭터 정보 저장
        if (data.Count > 0)
        {
            PlayerPrefs.SetString("LastCharacterNickName", data[0].NickName);
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs에 마지막 캐릭터 정보 저장 완료");
        }
    }

    // 전체 캐릭터 데이터 불러오기
    public List<PlayerData> LoadAllPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return new List<PlayerData>();
        }

        var json = File.ReadAllText(filePath);
        var wrapper = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json);
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
