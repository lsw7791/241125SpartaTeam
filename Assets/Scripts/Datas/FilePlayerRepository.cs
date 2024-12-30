using System.IO;
using UnityEngine;

public interface IPlayerRepository
{
    void Initialize();                  // �ʱ�ȭ
    void SaveCurrentPlayer(PlayerData data); // ���� �÷��� ĳ���� ����
    PlayerData LoadCurrentPlayer();    // ���� �÷��� ĳ���� �ε�
}

public class FilePlayerRepository : IPlayerRepository
{
    private string filePath;

    public void Initialize()
    {
        filePath = Path.Combine(Application.persistentDataPath, "CurrentPlayerData.json");

        if (!File.Exists(filePath))
        {
            Debug.Log($"������ �������� ����. ���: {filePath}");
            File.WriteAllText(filePath, string.Empty); // �� ���� ����
        }
        else
        {
            Debug.Log($"������ �����մϴ�. ���: {filePath}");
        }
    }

    public void SaveCurrentPlayer(PlayerData data)
    {
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
        Debug.Log($"���� �÷��� ĳ���� ���� �Ϸ�. ���: {filePath}");
    }

    public PlayerData LoadCurrentPlayer()
    {
        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(File.ReadAllText(filePath)))
        {
            Debug.LogWarning("����� ���� �÷��� ĳ���� �����Ͱ� �����ϴ�.");
            return null;
        }

        var json = File.ReadAllText(filePath);
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
