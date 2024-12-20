using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeDataManager : UpGradeData
{
    // 특정 ID의 아이템 데이터를 반환
    public UpGradeData GetData(int id)
    {
        if (UpGradeDataMap.ContainsKey(id))
        {
            return UpGradeDataMap[id];
        }
        return null;
    }

    // 특정 수치와 티어에 해당하는 데이터를 반환
    public UpGradeData GetUpgradeData(int inEnhence, int inTier)
    {
        return UpGradeDataList.Find(data => data.enhence == inEnhence && data.tier == inTier);
    }
}
