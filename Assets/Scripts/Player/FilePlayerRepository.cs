using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void SaveAllPlayerData(List<PlayerData> data); // ��ü ������ ����
    List<PlayerData> LoadAllPlayerData();          // ��ü ������ �ҷ�����
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    public void Initialize()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");

        // ������ �������� ������ �� ������ ������ ����
        if (!File.Exists(filePath))
        {
            Debug.Log($"������ �������� ����. ���: {filePath}");
            File.WriteAllText(filePath, JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(new List<PlayerData>())));
            Debug.Log("�� ������ ���� ���� �Ϸ�.");
        }
        else
        {
            Debug.Log($"������ �����մϴ�. ���: {filePath}");
        }
    }


    // ��ü ĳ���� ������ ����
    public void SaveAllPlayerData(List<PlayerData> data)
    {
        Debug.Log("PlayerData ���� ����");
        Debug.Log($"������ ������ ��: {data.Count}");
        foreach (var playerData in data)
        {
            Debug.Log($"������ �÷��̾�: {playerData.NickName}");
        }

        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(data));
        File.WriteAllText(filePath, json);
        Debug.Log($"PlayerData ���� �Ϸ�. ���: {filePath}");
    }


    // ��ü ĳ���� ������ �ҷ�����
    public List<PlayerData> LoadAllPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("����� �����Ͱ� �����ϴ�.");
            return new List<PlayerData>();
        }

        Debug.Log($"���� ���� Ȯ�� �Ϸ�. ���: {filePath}");

        // ������ ���Ͽ��� �о ��ȯ
        var json = File.ReadAllText(filePath);
        Debug.Log($"���Ͽ��� ���� JSON: {json}");

        var wrapper = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json);
        Debug.Log($"�ε�� ������ ��: {wrapper.List.Count}");

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
