using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static string SavePath(string fileName) => Path.Combine(Application.persistentDataPath, $"{fileName}.json");

    // �÷��̾� ������ ����
    public static void SavePlayerData(Player player, string fileName)
    {
        try
        {
            PlayerData data = new PlayerData
            {
                NickName = player.PlayerNickName,
                Stats = PlayerStatsData.FromPlayerStats(player.Stats),
                QuickSlotItems = player.QuickSlots.Slots,
            };

            string json = data.ToJson();
            File.WriteAllText(SavePath(fileName), json);
            Debug.Log($"�÷��̾� ������ ���� �Ϸ�: {SavePath(fileName)}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"�÷��̾� ������ ���� �� ���� �߻�: {ex.Message}");
        }
    }

    // �÷��̾� ������ �ε�
    public static void LoadPlayerData(Player player, string fileName)
    {
        try
        {
            string path = SavePath(fileName);
            if (!File.Exists(path))
            {
                Debug.LogWarning("����� �����Ͱ� �����ϴ�.");
                return;
            }

            string json = File.ReadAllText(path);
            PlayerData data = PlayerData.FromJson(json);

            player.PlayerNickName = data.NickName;
            player.stats = PlayerStatsData.ToPlayerStats(data.Stats);
            player.QuickSlots.Slots = new List<QuickSlotItem>(data.QuickSlotItems);

            Debug.Log("�÷��̾� ������ �ε� �Ϸ�");
        }
        catch (Exception ex)
        {
            Debug.LogError($"�÷��̾� ������ �ε� �� ���� �߻�: {ex.Message}");
        }
    }
}
