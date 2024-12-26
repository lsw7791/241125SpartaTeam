using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerSaveLoad
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "PlayerData.json");

    // 데이터 저장
    public static void SavePlayerData(Player player, IPlayerRepository repository)
    {
        if (player == null)
        {
            Debug.LogWarning("플레이어 데이터 저장 실패: 플레이어가 null입니다.");
            return;
        }

        try
        {
            PlayerData data = new PlayerData
            {
                NickName = player.PlayerNickName,
                MaxHP = player.Stats.MaxHP,
                CurrentHP = player.Stats.CurrentHP,
                CurrentStamina = player.Stats.CurrentStamina,
                Gold = player.Stats.Gold,
                Damage = player.Stats.Damage,
                MoveSpeed = player.Stats.MoveSpeed,
                ATKSpeed = player.Stats.ATKSpeed,
                Def = player.Stats.Def,
                WeaponType = player.Stats.WeaponType,
                Items = player.Stats.Items,
            };
            // JSON 변환
            string json = data.ToJson();

            // 디버그: 저장될 JSON 출력
            Debug.Log($"플레이어 데이터 JSON: {json}");

            // 파일로 저장
            File.WriteAllText(SavePath, json);

            // 디버그: 저장 경로와 성공 메시지 출력
            Debug.Log($"플레이어 데이터 저장 성공: {SavePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"플레이어 데이터 저장 실패: {ex.Message}");
        }
    }


    // 데이터 로드
    public static void LoadPlayerData(Player player, IPlayerRepository repository)
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return;
        }

        try
        {
            string json = File.ReadAllText(SavePath);

            Debug.Log(json);
            PlayerData data = PlayerData.FromJson(json);


            // 데이터 초기화 및 적용
            ApplyPlayerData(player, data);
            Debug.Log("플레이어 데이터 로드 성공");
        }
        catch (Exception ex)
        {
            Debug.LogError($"플레이어 데이터 로드 실패: {ex.Message}");
        }
    }

    // 플레이어 데이터 적용
    private static void ApplyPlayerData(Player player, PlayerData data)
    {
        if (data == null || player == null)
        {
            Debug.LogWarning("데이터 적용 실패: 데이터 또는 플레이어가 null입니다.");
            return;
        }

        // 스탯 적용
        player.Stats.MaxHP = data.MaxHP;
        player.Stats.CurrentHP = data.CurrentHP;
        player.Stats.CurrentStamina = data.CurrentStamina;
        player.Stats.Gold = data.Gold;
        player.Stats.Damage = data.Damage;
        player.Stats.MoveSpeed = data.MoveSpeed;
        player.Stats.ATKSpeed = data.ATKSpeed;
        player.Stats.Def = data.Def;
        player.Stats.WeaponType = data.WeaponType;

        // 닉네임 적용
        player.PlayerNickName = data.NickName;
    }
}
