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
    HealthPotion,      // 체력 포션
    StaminaPotion      // 스태미나 포션
}

public enum ItemUseType
{ // 인벤토리에서 슬롯을 눌렀을 때 구분할 타입
    Consumable,
    Equipment,
    Material,
}

// 아이템 데이터를 관리하는 매니저 클래스
public class ItemDataManager : ItemData
{ // 아이템 데이터 관리 및 반환
    // 모든 아이템 데이터를 반환
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

    // 랜덤으로 아이템 드랍 데이터를 반환
    public ItemData GetRandomDropItem(List<int> dropItemIds)
    {
        List<ItemData> validItems = new List<ItemData>();

        // 드랍할 수 있는 아이템 데이터를 필터링
        foreach (int id in dropItemIds)
        {
            if (ItemDataMap.ContainsKey(id))
            {
                validItems.Add(ItemDataMap[id]);
            }
        }

        if (validItems.Count > 0)
        {
            // 랜덤으로 드랍 아이템 선택
            int randomIndex = Random.Range(0, validItems.Count);
            return validItems[randomIndex];
        }

        return null;
    }
}
