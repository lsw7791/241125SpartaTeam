using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static string SavePath(string fileName) => Path.Combine(Application.persistentDataPath, $"{fileName}.json");

    // 플레이어 데이터 저장
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
            Debug.Log($"플레이어 데이터 저장 완료: {SavePath(fileName)}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"플레이어 데이터 저장 중 오류 발생: {ex.Message}");
        }
    }

    // 플레이어 데이터 로드
    public static void LoadPlayerData(Player player, string fileName)
    {
        try
        {
            string path = SavePath(fileName);
            if (!File.Exists(path))
            {
                Debug.LogWarning("저장된 데이터가 없습니다.");
                return;
            }

            string json = File.ReadAllText(path);
            PlayerData data = PlayerData.FromJson(json);

            player.PlayerNickName = data.NickName;
            player.stats = PlayerStatsData.ToPlayerStats(data.Stats);
            player.QuickSlots.Slots = new List<QuickSlotItem>(data.QuickSlotItems);

            Debug.Log("플레이어 데이터 로드 완료");
        }
        catch (Exception ex)
        {
            Debug.LogError($"플레이어 데이터 로드 중 오류 발생: {ex.Message}");
        }
    }
}
