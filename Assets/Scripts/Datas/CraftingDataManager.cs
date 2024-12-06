using MainData;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataManager : CraftingData
{

   
    // 특정 아이템 ID에 해당하는 크래프팅 데이터를 반환합니다.
    public CraftingData GetData(int id)
    {
        if (CraftingDataMap.TryGetValue(id, out var data))
        {
            return data;
        }
        Debug.LogWarning($"ID {id}에 해당하는 크래프팅 데이터가 존재하지 않습니다.");
        return null;
    }

    // 모든 크래프팅 데이터 반환
    public List<CraftingData> GetAllDatas()
    {
        return CraftingDataList;
    }

    // 특정 티어에 해당하는 데이터를 반환
    public List<CraftingData> GetDataByTier(int tier)
    {
        return CraftingDataList.FindAll(data => data.tier == tier);
    }

    // 특정 아이템에 필요한 재료 정보를 반환
    public Dictionary<int, int> GetRequiredMaterials(int id)
    {
        var data = GetData(id);
        if (data == null) return null;

        var materials = new Dictionary<int, int>();

        if (data.resourceMine > 0) materials.Add(1, data.resourceMine);  // 자원 1: 광물
        if (data.resourceLadder > 0) materials.Add(2, data.resourceLadder);  // 자원 2: 가죽
        if (data.resourceOther > 0) materials.Add(3, data.resourceOther);  // 자원 3: 기타
        if (data.resourceJewel > 0) materials.Add(4, data.resourceJewel);  // 자원 4: 보석

        return materials;
    }
}
