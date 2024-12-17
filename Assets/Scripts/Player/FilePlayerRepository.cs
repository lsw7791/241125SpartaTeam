using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void Initialize();             // Initialize �޼��� �߰�
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
        var json = JsonUtility.ToJson(new SerializableListWrapper<PlayerData>(data));
        File.WriteAllText(filePath, json);
        Debug.Log($"PlayerData ���� �Ϸ�. ���: {filePath}");

        // PlayerPrefs�� ������ ĳ���� ���� ����
        if (data.Count > 0)
        {
            PlayerPrefs.SetString("LastCharacterNickName", data[0].NickName);
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs�� ������ ĳ���� ���� ���� �Ϸ�");
        }
    }

    // ��ü ĳ���� ������ �ҷ�����
    public List<PlayerData> LoadAllPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("����� �����Ͱ� �����ϴ�.");
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
