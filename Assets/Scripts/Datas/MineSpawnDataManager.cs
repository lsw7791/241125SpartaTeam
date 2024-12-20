using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawnDataManager : MineSpawnData
{
    public MineSpawnData GetData(int id)
    {
        return MineSpawnDataMap[id];
    }

    public List<int> GetMIneID(int id)
    {
        return MineSpawnDataMap[id].MineID;
    }

    public string GetMInePosition(int id)
    {
        return MineSpawnDataMap[id].MinePosition;
    }

}
