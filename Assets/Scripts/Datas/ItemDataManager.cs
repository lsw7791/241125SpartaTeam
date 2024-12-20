using System.Collections.Generic;
using UnityEngine;
using MainData;
using GoogleSheet.Core.Type;

// 구글 시트의 "ItemType" 열과 매핑되는 열거형
[UGS(typeof(ItemType))]
public enum ItemType
{
    Weapon = 1,         // 검
    Shield,            // 방패
    Pickaxe,           // 곡괭이
    Helmet,            // 헬멧
    Top,               // 상의
    Bottom,            // 하의
    Armor,             // 갑옷
    Cape,              // 망토
    Mine,              // 광산
    Jewel,             // 보석
    Ladder,            // 사다리
    Other,             // 기타
    Gold,              // 재화
    Potion,      //포션
}

//public enum ItemUseType
//{ // 인벤토리에서 슬롯을 눌렀을 때 구분할 타입
//    Consumable,
//    Equipment,
//    Material,
//}

// 아이템 데이터를 관리하는 매니저 클래스
public class ItemDataManager : ItemData
{
    // 아이템 데이터 리스트를 반환
    public List<ItemData> GetItemDatas()
    {
        return ItemDataList;
    }

    // 특정 ID의 아이템 데이터를 반환
    public ItemData GetData(int id)
    {
        if (ItemDataMap.ContainsKey(id))
        {
            return ItemDataMap[id];
        }
        return null;
    }

    // 특정 타입(ItemType)의 모든 아이템 데이터를 반환
    public List<ItemData> GetDataByType(ItemType type)
    {
        return ItemDataList.FindAll(x => x.itemType == type);
    }

    // 포션을 사용하여 플레이어의 HP와 스태미너를 증가시킴
    public void UsePotion(int itemId)
    {
        // 포션 아이템 데이터를 가져옴
        ItemData potionData = GetData(itemId);

        // 아이템이 null이거나 포션이 아닌 경우
        if (potionData == null || potionData.itemType != ItemType.Potion)
        {
            Debug.LogError("Invalid item ID or not a Potion.");
            return;
        }

        // 포션의 효과 (Health와 Stamina 값) 가져오기
        int healthIncrease = potionData.health;
        int staminaIncrease = potionData.stamina;

        // 플레이어의 HP와 Stamina 업데이트
        var playerStats = GameManager.Instance.Player.stats;

        playerStats.CurrentHP = Mathf.Min(playerStats.MaxHP, playerStats.CurrentHP + healthIncrease);
        playerStats.CurrentStamina = Mathf.Min(playerStats.MaxStamina, playerStats.CurrentStamina + staminaIncrease);

        // 디버깅: 효과 적용 후 값 확인
        Debug.Log($"Potion used: Health +{healthIncrease}, Stamina +{staminaIncrease}");
        Debug.Log($"Player stats updated: HP = {playerStats.CurrentHP}, Stamina = {playerStats.CurrentStamina}");
    }
}

