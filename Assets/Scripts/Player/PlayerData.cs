using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string NickName;
    public int CurrentHP;
    public int CurrentStamina;
    public int CurrentGold;
    public int CurrentDamage;
    public int CurrentSpeed;
    public float CurrentATKSpeed;
    public int CurrentDef;
    public int CurrentWeaponType;
    public List<QuickSlotItem> QuickSlotItems = new List<QuickSlotItem>();  // QuickSlotItem 리스트로 수정

    // PlayerStats와 Inventory 추가
    public PlayerStatsData Stats;  // PlayerStats의 데이터를 저장
    public InventoryData Inventory; // PlayerInventory의 데이터를 저장

    // JSON 직렬화
    public string ToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    // JSON 역직렬화
    public static PlayerData FromJson(string json)
    {
        return JsonUtility.FromJson<PlayerData>(json);
    }
}
