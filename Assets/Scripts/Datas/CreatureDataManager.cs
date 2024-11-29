using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDataManager : CreatureData
{
    public CreatureData GetData(int id)
    {
        return CreatureDataMap[id];
    }

    public List<CreatureData> GetDataByType()
    {
        //return CreatureDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
