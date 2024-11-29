using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolDataManager : ToolData
{
    public ToolData GetItemData(int id)
    {
        return ToolDataMap[id];
    }

    public List<ToolData> GetItemDataByType()
    {
        //return ToolDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
