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

    // Initialize �޼��� �߰�
    public void Initialize()
    {
        filePath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        if (!File.Exists(filePath))
        {
            // ������ �������� ������ �� JSON �迭�� ����
            File.WriteAllText(filePath, JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(new List<PlayerData>())));
        }
    }

    public void SaveAllPlayerData(List<PlayerData> data)
    {
        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(data));
        File.WriteAllText(filePath, json);
        Debug.Log("PlayerData ���� �Ϸ�");
    }

    public List<PlayerData> LoadAllPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("����� �����Ͱ� �����ϴ�.");
            return new List<PlayerData>();
        }

        var json = File.ReadAllText(filePath);
        var wrapper = JsonUtility.FromJson<SerializableListWrapper<PlayerData>>(json);
        Debug.Log("PlayerData �ε� �Ϸ�");
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

