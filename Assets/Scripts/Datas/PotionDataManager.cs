using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using GoogleSheet.Core.Type;
[UGS(typeof(PotionType))]
public enum PotionType
{
    HealthPotion = 1,
    StaminaPotion
}
public class PotionDataManager : PotionData
{
    public PotionData GetData(int id)
    {
        return PotionDataMap[id];
    }

    public List<PotionData> GetDataByType()
    {
        //return PotionDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
