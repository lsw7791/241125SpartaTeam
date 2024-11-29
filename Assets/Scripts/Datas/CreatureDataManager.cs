using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
public class CreatureDataManager : CreatureData
{
    public CreatureData GetItemData(int id)
    {
        return CreatureDataMap[id];
    }

    public List<CreatureData> GetItemDataByType()
    {
        //return Creature1DataList.FindAll(x=> x.type == 1)
        return null;
    }
}
