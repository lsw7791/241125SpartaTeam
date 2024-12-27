using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDataManager : UIData
{
    public List<UIData> GetItemDatas()
    {
        return UIDataList;
    }

    // 특정 ID의 아이템 데이터를 반환
    public UIData GetData(int id)
    {
        if (UIDataMap.ContainsKey(id))
        {
            return UIDataMap[id];
        }
        return null;
    }
    public string GetAtlasData(int id)
    {
        if (UIDataMap.ContainsKey(id))
        {
            return UIDataMap[id].atlasPath;
        }
        return null;
    }
}
