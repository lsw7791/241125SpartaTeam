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
                Speed = player.Stats.Speed,
                ATKSpeed = player.Stats.ATKSpeed,
                Def = player.Stats.Def,
                WeaponType = player.Stats.WeaponType,
                QuickSlotItems = new List<QuickSlotItem>(), // QuickSlotItems 저장
                InventoryItems = new List<InventoryItem>(), // Inventory 저장
            };

            // QuickSlotItems 저장
            foreach (QuickSlotItem item in player.QuickSlots.Slots)
            {
                data.QuickSlotItems.Add(item);
            }

            // Inventory 아이템 저장
            foreach (InventoryItem item in player.Inventory.Items)
            {
                data.InventoryItems.Add(new InventoryItem
                {
                    ItemID = item.ItemID,
                    //ItemName = item.ItemName,
                    //Quantity = item.Quantity,
                    //ItemType = item.ItemType,
                    //ItemIcon = null,
                    // ItemUseType = item.ItemUseType,
                    //IsEquipped = item.IsEquipped,
                });
            }

            string json = data.ToJson();
            File.WriteAllText(SavePath, json);
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
        player.Stats.Speed = data.Speed;
        player.Stats.ATKSpeed = data.ATKSpeed;
        player.Stats.Def = data.Def;
        player.Stats.WeaponType = data.WeaponType;

        // 닉네임 적용
        player.PlayerNickName = data.NickName;

        // QuickSlotItems 적용
        player.QuickSlots.Slots.Clear();
        foreach (QuickSlotItem item in data.QuickSlotItems)
        {
            player.QuickSlots.Slots.Add(item);
        }

        // Inventory 적용
        player.Inventory.Items.Clear();
        foreach (InventoryItem itemData in data.InventoryItems)
        {
            player.Inventory.AddItem(
                itemData.ItemID,
                itemData.ItemName,
                itemData.Quantity,
                itemData.ItemType,
                null // Sprite는 null로 설정
                //itemData.EquipSlot
            );
        }
    }
}
