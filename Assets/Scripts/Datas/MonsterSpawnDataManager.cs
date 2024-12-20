using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnDataManager : MonsterSpawnData
{
    public MonsterSpawnData GetData(int id)
    {
        return MonsterSpawnDataMap[id];
    }
    public List<int> GetMonsterID(int id)
    {
        return MonsterSpawnDataMap[id].MonsterID;
    }

    public string GetMonsterPosition(int id)
    {
        return MonsterSpawnDataMap[id].MonsterPosition;
    }
}
