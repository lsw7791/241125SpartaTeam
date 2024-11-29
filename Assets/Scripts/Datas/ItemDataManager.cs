using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;

public class ItemDataManager : ItemData
{
    public ItemData GetData(int id)
    {
        return ItemDataMap[id];
    }

    public List<ItemData> GetDataByType()
    {
        //return ItemDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
