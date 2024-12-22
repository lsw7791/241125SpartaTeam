using MainData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpGradeDataManager : UpGradeData
{
    public UpGradeData GetData(int id)
    {
        if (UpGradeDataMap.ContainsKey(id))
        {
            return UpGradeDataMap[id];
        }
        return null;
    }
}
