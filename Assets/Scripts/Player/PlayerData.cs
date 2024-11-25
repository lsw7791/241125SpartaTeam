﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public string ItemName;   // 아이템 이름
    public int Quantity;      // 아이템 개수
    public string ItemType;   // 아이템 종류 (예: 무기, 포션 등)
    public int SlotIndex;     // 슬롯 번호

    public InventoryItem(string itemName, int quantity, string itemType, int slotIndex)
    {
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        SlotIndex = slotIndex;
    }
}

[Serializable]
public class QuickSlotItem
{
    public string ItemName;   // 아이템 이름
    public int SlotIndex;     // 퀵슬롯 번호

    public QuickSlotItem(string itemName, int slotIndex)
    {
        ItemName = itemName;
        SlotIndex = slotIndex;
    }
}

[Serializable]
public class PlayerData
{
    public int HP;                        // 체력
    public int Stamina;                   // 스태미나
    public int Gold;                      // 골드
    public int Damage;                    // 공격력
    public int Speed;                     // 이동 속도
    public float AttackSpeed;             // 공격 속도
    public int Def;                       // 방어력
    public string NickName;               // 플레이어 닉네임
    public int Lv;                        // 레벨
    public int MineLv;                    // 광산 레벨
    public List<InventoryItem> Inventory; // 인벤토리 아이템 목록
    public List<QuickSlotItem> QuickSlots; // 퀵슬롯 아이템 목록

    public PlayerData()
    {
        // 리스트를 초기화
        Inventory = new List<InventoryItem>();
        QuickSlots = new List<QuickSlotItem>();
    }

    // JSON 직렬화
    public string ToJson()
    {
        return JsonUtility.ToJson(this, true); // 포맷을 예쁘게 저장
    }

    // JSON 역직렬화
    public static PlayerData FromJson(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
