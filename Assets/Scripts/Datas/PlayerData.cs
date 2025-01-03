﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string NickName;        // 캐릭터 이름
    public int MaxHP;              // 최대 HP
    public int CurrentHP;
    public int MaxStamina;         // 최대 스태미나
    public int CurrentStamina;     // 현재 스태미나
    public int Gold;               // 현재 금액
    public int Damage;             // 일반 공격력
    public int PhysicalDamage;     // 물리 공격력
    public int MagicalDamage;      // 마법 공격력

    public float MoveSpeed;        // 이동 속도
    public float ATKSpeed;         // 현재 공격 속도
    public int Def;                // 현재 방어력
    public int CurrentDef;         // 현재 방어력

    public int WeaponType;         // 현재 무기 타입
    public bool isDie;             // 사망 여부
    public int MineDamage;         // 광물 공격력
    public List<InventoryItem> Items;  // 플레이어가 소유한 아이템 목록

    // PlayerStats 초기화 메서드 (초기값 설정)
    public void Initialize()
    {
        Gold = 0;
        isDie = false;
        Items = new List<InventoryItem>();
        PlayerStatsReset();

    }
    public void PlayerStatsReset()
    {
        var PlayerStats = GameManager.Instance.DataManager.PlayerStat.GetData(1);

        MaxHP = PlayerStats.maxHP;
        CurrentHP = MaxHP;
        MaxStamina = PlayerStats.maxStamina;
        CurrentStamina = MaxStamina;
        Damage = PlayerStats.damage;
        PhysicalDamage = PlayerStats.physicalDamage;
        MagicalDamage = PlayerStats.magicalDamage;
        MoveSpeed = PlayerStats.moveSpeed;
        ATKSpeed = PlayerStats.atkSpeed;
        Def = PlayerStats.def;
        CurrentDef = Def;
        MineDamage = PlayerStats.mineDamage;
        WeaponType = 0;
    }
    public void PlayerStatsUpdate(InventoryItem inItem, bool isEquip)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int enhenceCount = inItem.enhenceCount + 1;
        PlayerData nowPlayer = GameManager.Instance.DataManager.nowPlayer;

        int equip = isEquip ? 1 : -1;

        nowPlayer.MaxHP += itemData.health * enhenceCount * equip;
        nowPlayer.MaxStamina += itemData.stamina * enhenceCount * equip;
        nowPlayer.CurrentDef += itemData.defense * enhenceCount * equip;
        nowPlayer.Def += itemData.defense * enhenceCount * equip;
        nowPlayer.MineDamage += itemData.attackMine * enhenceCount * equip;
        nowPlayer.PhysicalDamage += itemData.attack * enhenceCount * equip;
        nowPlayer.MagicalDamage += itemData.attackM * enhenceCount * equip;
        nowPlayer.ATKSpeed += itemData.attackSpeed * enhenceCount * equip;
        nowPlayer.MoveSpeed += itemData.moveSpeed * enhenceCount * equip;

        if (UIManager.Instance.IsExistUI<StatusUI>())
        {
            StatusUI statusUI = UIManager.Instance.GetUI<StatusUI>();

            statusUI.Refresh();
        }
        else
        {
            StatusUI statusUI = UIManager.Instance.GetUI<StatusUI>();
            statusUI.gameObject.SetActive(false);
            statusUI.Refresh();
        }
    }

    // 아이템 추가 메서드
    public void AddItem(InventoryItem newItem)
    {
        // 인벤토리에 아이템을 추가하고 스탯을 갱신
        Items.Add(newItem);
    }

    // 아이템 제거 메서드
    public void RemoveItem(InventoryItem item)
    {
        // 아이템 제거 후 스탯을 갱신
        Items.Remove(item);
    }

}
