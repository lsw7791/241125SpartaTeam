using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheet.Core.Type;
[UGS(typeof(CreatureType))]
public enum CreatureType
{
    MeleeMonster = 1,//근거리
    RangedMonster,//원거리
    ChargedMonster, //돌진
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
    public CreatureType GetType(int id)
    {
        return CreatureDataMap[id].creatureType;
    }
    public string GetName(int id)
    {
        return CreatureDataMap[id].name;
    }
    public string GetDesc(int id)
    {
        return CreatureDataMap[id].desc;
    }
    public int GetTier(int id)
    {
        return CreatureDataMap[id].tier;
    }
    public int GetAttack(int id)
    {
        return CreatureDataMap[id].attack;
    }
    public int GetDefense(int id)
    {
        return CreatureDataMap[id].defense;
    }
    public int GetHealth(int id)
    {
        return CreatureDataMap[id].health;
    }
    public float GetAttackSpeed(int id)
    {
        return CreatureDataMap[id].attackSpeed;
    }
    public float GetDetectionRange(int id)
    {
        return CreatureDataMap[id].detectionRange;
    }
    public float GetAttackRange(int id)
    {
        return CreatureDataMap[id].attackRange;
    }
    public float GetMoveSpeed(int id)
    {
        return CreatureDataMap[id].moveSpeed;
    }
    public float GetDrop(int id)
    {
        return CreatureDataMap[id].drop;
    }
    public List<int> GetDropItemIds(int id)
    {
        return CreatureDataMap[id].dropItemIds;
    }
    public string GetPrefabsPath(int id)
    {
        return CreatureDataMap[id].prefabsPath;
    }
}
