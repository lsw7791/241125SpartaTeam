using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheet.Core.Type;
[UGS(typeof(CreatureType))]
public enum CreatureType
{
    MonsterMelee = 1,//근거리
    MonsterRanged,//원거리
   Mine,
   Jewel
}
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
