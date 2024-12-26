using System.Collections.Generic;
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
    public int WeaponType;         // 현재 무기 타입
    public bool isDie;             // 사망 여부
    public int MineDamage;         // 광물 공격력
    public List<InventoryItem> Items;  // 플레이어가 소유한 아이템 목록

    // JSON 직렬화
    public string ToJson() => JsonUtility.ToJson(this, true);
    public static PlayerData FromJson(string json) => JsonUtility.FromJson<PlayerData>(json);

    // PlayerStats 초기화 메서드 (초기값 설정)
    public void Initialize()
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
        MaxStamina = 100;
        CurrentStamina = 100;
        Gold = 0;
        Damage = 10;
        PhysicalDamage = 0;
        MagicalDamage = 0;
        MoveSpeed = 3;
        ATKSpeed = 1;
        Def = 0;
        MineDamage = 1;
        WeaponType = 0;
        isDie = false;
        Items = new List<InventoryItem>();
    }

    // 아이템 장착 시 스탯 업데이트
    public void PlayerStatsEquip(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        MaxHP += itemData.health;
        MaxStamina += itemData.stamina;
        Def += itemData.defense;
        MineDamage += itemData.attackMine;
        PhysicalDamage += itemData.attack;
        MagicalDamage += itemData.attackM;
        ATKSpeed += itemData.attackSpeed;
        MoveSpeed += itemData.moveSpeed;

        // UI 갱신
        UIManager.Instance.ToggleUI<StatusUI>();
        GameManager.Instance.Player.StatusUI.Refresh();
        UIManager.Instance.ToggleUI<StatusUI>();
    }

    // 아이템 해제 시 스탯 업데이트
    public void PlayerStatsUnEquip(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        MaxHP -= itemData.health;
        MaxStamina -= itemData.stamina;
        Def -= itemData.defense;
        MineDamage -= itemData.attackMine;
        PhysicalDamage -= itemData.attack;
        MagicalDamage -= itemData.attackM;
        ATKSpeed -= itemData.attackSpeed;
        MoveSpeed -= itemData.moveSpeed;

        // UI 갱신
        GameManager.Instance.Player.StatusUI.Refresh();
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
