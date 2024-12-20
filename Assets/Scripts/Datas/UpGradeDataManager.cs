using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeDataManager : UpGradeData
{
    // Ư�� ID�� ������ �����͸� ��ȯ
    public UpGradeData GetData(int id)
    {
        if (UpGradeDataMap.ContainsKey(id))
        {
            return UpGradeDataMap[id];
        }
        return null;
    }

    // Ư�� ��ġ�� Ƽ� �ش��ϴ� �����͸� ��ȯ
    public UpGradeData GetUpgradeData(int inEnhence, int inTier)
    {
        return UpGradeDataList.Find(data => data.enhence == inEnhence && data.tier == inTier);
    }
}
