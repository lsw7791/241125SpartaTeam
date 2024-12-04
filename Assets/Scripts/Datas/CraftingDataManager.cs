using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataManager : CraftingData
{
    // 특정 무기 ID에 해당하는 크래프팅 데이터를 반환합니다.
    public CraftingData GetData(int id)
    {
        if (CraftingDataMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"ID {id}에 해당하는 크래프팅 데이터가 존재하지 않습니다.");
        return null;
    }

    // 특정 타입(예: 무기 유형, 티어 등)에 따른 데이터를 반환합니다.
    public List<CraftingData> GetDataByTier(int tier)
    {
        return CraftingDataList.FindAll(data => data.tier == tier);
    }

    // 특정 아이템 ID의 필요한 재료 정보를 반환합니다.
    public Dictionary<int, int> GetRequiredMaterials(int id)
    {
        var data = GetData(id);
        if (data == null)
        {
            return null;
        }

        var materials = new Dictionary<int, int>();

        if (data.resourceMine > 0) materials.Add(1, data.resourceMine);  // 자원 1: 광물
        if (data.resourceLadder > 0) materials.Add(2, data.resourceLadder);  // 자원 2: 사다리
        if (data.resourceOther > 0) materials.Add(3, data.resourceOther);  // 자원 3: 기타
        if (data.resourceJewel > 0) materials.Add(4, data.resourceJewel);  // 자원 4: 보석

        return materials;
    }

    // 크래프팅을 시도하고 결과를 반환합니다.
    public bool TryCraftItem(int id)
    {
        var requiredMaterials = GetRequiredMaterials(id);

        if (requiredMaterials == null || requiredMaterials.Count == 0)
        {
            Debug.LogWarning($"ID {id}에 필요한 재료 정보가 없습니다.");
            return false;
        }

        // 인벤토리에서 재료를 확인합니다.
        if (!InventoryManager.Instance.HasRequiredMaterials(requiredMaterials))
        {
            Debug.Log("재료가 부족합니다!");
            return false;
        }

        // 재료 차감 및 아이템 생성
        InventoryManager.Instance.ConsumeMaterials(requiredMaterials);
        InventoryManager.Instance.AddItem(id, 1);

        Debug.Log($"아이템 ID {id} 제작 성공!");
        //UIManager.Instance.RefreshUI();  // UI 업데이트
        return true;
    }
}
