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
        MineDamage = PlayerStats.mineDamage;
        WeaponType = 0;
    }
    public void PlayerStatsUpdate(InventoryItem inItem, bool isEquip)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);
        int enhenceCount = inItem.enhenceCount + 1;

        MaxHP += isEquip ? itemData.health * enhenceCount : itemData.health * enhenceCount * -1;
        MaxStamina += isEquip ? itemData.stamina * enhenceCount : itemData.stamina * enhenceCount * -1;
        Def += isEquip ? itemData.defense * enhenceCount : itemData.defense * enhenceCount * -1;
        MineDamage += isEquip ? itemData.attackMine * enhenceCount : itemData.attackMine * enhenceCount * -1;
        PhysicalDamage += isEquip ? itemData.attack * enhenceCount : itemData.attack * enhenceCount * -1;
        MagicalDamage += isEquip ? itemData.attackM * enhenceCount : itemData.attackM * enhenceCount * -1;
        ATKSpeed += isEquip ? itemData.attackSpeed * enhenceCount : itemData.attackSpeed * enhenceCount * -1;
        MoveSpeed += isEquip ? itemData.moveSpeed * enhenceCount : itemData.moveSpeed * enhenceCount * -1;

        if (UIManager.Instance.IsExistUI<StatusUI>())
        {
            GameManager.Instance.Player.StatusUI.Refresh();
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
