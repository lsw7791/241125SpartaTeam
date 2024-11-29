using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
public class CreatureDataManager : Creature1Data
{
    public Creature1Data GetItemData(int id)
    {
        return Creature1DataMap[id];
    }

    public List<Creature1Data> GetItemDataByType()
    {
        //return Creature1DataList.FindAll(x=> x.type == 1)
        return null;
    }
}
