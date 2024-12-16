using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void SavePlayerData(List<PlayerData> data);  // List<PlayerData> �޵��� ����
    List<PlayerData> LoadPlayerData();  // List<PlayerData> ��ȯ�ϵ��� ����
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    // �����ڿ��� persistentDataPath�� ������� ����
    public FilePlayerRepository()
    {
        Debug.Log("FilePlayerRepository ������ ȣ��");  // ������ ȣ�� �α�
    }

    // Initialize() �޼��忡�� persistentDataPath ���
    public void Initialize()
    {
        Debug.Log("FilePlayerRepository Initialize ȣ��");  // Initialize ȣ�� �α�
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");  // ������ ������ �� JSON �迭�� �ʱ�ȭ
        }
    }

    public void SavePlayerData(List<PlayerData> data)
    {
        string json = JsonUtility.ToJson(new PlayerDataListWrapper { playerDataList = data });
        File.WriteAllText(filePath, json);  // savePath ��� filePath ���
        Debug.Log($"�÷��̾� ������ ����: {filePath}");
    }

    public List<PlayerData> LoadPlayerData()
    {
        if (!File.Exists(filePath))  // savePath ��� filePath ���
        {
            Debug.LogWarning("����� �����Ͱ� �����ϴ�.");
            return new List<PlayerData>(); // �����Ͱ� ������ �� ����Ʈ ��ȯ
        }

        string json = File.ReadAllText(filePath);  // savePath ��� filePath ���
        return JsonUtility.FromJson<PlayerDataListWrapper>(json).playerDataList;
    }
}

// PlayerData ����Ʈ�� �����ϴ� Ŭ���� �߰� (JsonUtility���� ����Ʈ�� ����ȭ�� �� �ֵ���)
[System.Serializable]
public class PlayerDataListWrapper
{
    public List<PlayerData> playerDataList;
}
