using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingDataManager : CraftingData
{
    public CraftingData GetData(int id)
    {
        return CraftingDataMap[id];
    }

    public List<CraftingData> GetDataByType()
    {
        //return TableDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
