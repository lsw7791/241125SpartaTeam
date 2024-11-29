using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;

public class PotionDataManager : PotionData
{
    public PotionData GetItemData(int id)
    {
        return PotionDataMap[id];
    }

    public List<PotionData> GetItemDataByType()
    {
        //return PotionDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
