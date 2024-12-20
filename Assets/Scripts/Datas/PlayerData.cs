using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string NickName;        // 캐릭터 이름
    public int MaxHP;              // 최대 HP
    public int CurrentHP;
    public int MaxStamina;              // 최대 HP
    public int CurrentStamina;     // 현재 스태미나
    public int Gold;        // 현재 금액
    public int Damage;      // 일반 공격력
    public int WeaponDamage; // 무기 공격력
    public int Speed;       // 이동 속도
    public float ATKSpeed;  // 현재 공격 속도
    public int Def;         // 현재 방어력
    public int WeaponType;  // 현재 무기 타입
    public bool isDie;             // 사망 여부
    public int MineDamage;

    public List<QuickSlotItem> QuickSlotItems = new List<QuickSlotItem>(); // 퀵슬롯 아이템 목록
    public List<InventoryItem> InventoryItems = new List<InventoryItem>(); // 인벤토리 아이템 목록

    public string ToJson() => JsonUtility.ToJson(this, true);  // JSON 직렬화
    public static PlayerData FromJson(string json) => JsonUtility.FromJson<PlayerData>(json);  // JSON 역직렬화

    // PlayerStats 초기화 메서드 (초기값 설정)
    public void Initialize()
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
        MaxStamina = 100;
        CurrentStamina = 100;
        Gold = 0;
        Damage = 10;
        WeaponDamage = 0;
        Speed = 3;
        ATKSpeed = 1;
        Def = 0;
        MineDamage = 1;
        WeaponType = 0;
        isDie = false;
    }
}
