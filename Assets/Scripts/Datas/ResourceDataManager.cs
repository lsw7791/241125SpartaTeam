using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
public class ResourceDataManager : ResourceData
{
    public ResourceData GetItemData(int id)
    {
        return ResourceDataMap[id];
    }

    public List<ResourceData> GetItemDataByType()
    {
        //return ResourceDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
