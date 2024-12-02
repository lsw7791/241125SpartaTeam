using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerSaveLoad
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "PlayerData.json");

    // 저장
    public static void SavePlayerData(Player player, IPlayerRepository repository)
    {
        PlayerData data = new PlayerData
        {
            NickName = player.PlayerNickName,  // 닉네임 저장
            CurrentHP = player.Stats.HP,
            CurrentStamina = player.Stats.Stamina,
            CurrentGold = player.Stats.Gold,
            CurrentDamage = player.Stats.Damage,
            CurrentSpeed = player.Stats.Speed,
            CurrentATKSpeed = player.Stats.AttackSpeed,
            CurrentDef = player.Stats.Defense,
            CurrentWeaponType = player.Stats.WeaponType,
            QuickSlotItems = new List<QuickSlotItem>(),  // QuickSlotItems 리스트

            Stats = new PlayerStatsData
            {
                HP = player.Stats.HP,
                Stamina = player.Stats.Stamina,
                Damage = player.Stats.Damage,
                Speed = player.Stats.Speed,
                AttackSpeed = player.Stats.AttackSpeed,
                Defense = player.Stats.Defense,
                WeaponType = player.Stats.WeaponType
            },

            Inventory = new InventoryData()
        };

        // QuickSlotItems 저장 (QuickSlotItem 객체로 저장)
        foreach (QuickSlotItem item in player.QuickSlots.Slots)  // QuickSlot을 사용하고 Slots로 접근
        {
            data.QuickSlotItems.Add(item);  // QuickSlotItem 객체 그대로 저장
        }

        // 인벤토리 아이템도 저장
        foreach (InventoryItem item in player.Inventory.Items)
        {
            data.Inventory.ItemIDs.Add(item.ItemID.ToString());  // InventoryItem에서 ItemID는 string으로 저장
        }

        string json = data.ToJson();
        File.WriteAllText(SavePath, json);
        Debug.Log($"플레이어 데이터 저장: {SavePath}");
    }

    // 로드
    public static void LoadPlayerData(Player player, IPlayerRepository repository)
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("저장된 데이터가 없습니다.");
            return;
        }

        string json = File.ReadAllText(SavePath);
        PlayerData data = PlayerData.FromJson(json);

        // PlayerStats 로드
        player.Stats.HP = data.CurrentHP;
        player.Stats.Stamina = data.CurrentStamina;
        player.Stats.Damage = data.CurrentDamage;
        player.Stats.Speed = data.CurrentSpeed;
        player.Stats.AttackSpeed = data.CurrentATKSpeed;
        player.Stats.Defense = data.CurrentDef;
        player.Stats.WeaponType = data.CurrentWeaponType;

        // 닉네임 로드
        player.PlayerNickName = data.NickName;  // NickName 로드 추가

        // QuickSlotItems 로드
        player.QuickSlots.Slots.Clear();  // 기존 슬롯 초기화
        foreach (QuickSlotItem item in data.QuickSlotItems)
        {
            player.QuickSlots.Slots.Add(item);  // QuickSlotItem 객체 그대로 추가
        }

        // Inventory 아이템 로드
        player.Inventory.Items.Clear();
        foreach (string itemID in data.Inventory.ItemIDs)
        {
            player.Inventory.AddItem(itemID, "Item Name", 1, "ItemType");  // ItemName과 ItemType은 예시로 설정
        }

        Debug.Log("플레이어 데이터 로드 완료");
    }
}
