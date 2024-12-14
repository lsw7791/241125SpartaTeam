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

    // 특정 아이템에 필요한 재료 정보를 반환 (matter와 count를 기반으로)
    public Dictionary<int, int> GetRequiredMaterials(int id)
    {
        var data = GetData(id);
        if (data == null) return null;

        var materials = new Dictionary<int, int>();

        // matter와 count 리스트를 기반으로 재료 정보를 동적으로 추가
        List<int> matter = data.matter; // 자원 종류
        List<int> count = data.count;   // 자원 갯수

        for (int i = 0; i < matter.Count; i++)
        {
            int itemId = matter[i];  // 자원 아이디
            int itemCount = count[i]; // 해당 자원의 필요한 수량

            // 재료가 있는 경우만 추가
            if (itemCount > 0)
            {
                materials.Add(itemId, itemCount);
            }
        }

        return materials;
    }

    public List<int> GetCraftItemIds(int id)
    {
        return CraftingDataMap[id].matter;
    }

    public List<int> GetCraftCountIds(int id)
    {
        return CraftingDataMap[id].count;
    }
}
