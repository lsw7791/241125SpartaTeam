using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheet.Core.Type;
[UGS(typeof(TableType))]
public enum TableType
{

    Sword=1,
    Bow,
    Staff,
    Shield,
    Pickaxe,
    Helmet,
    Top,
    Bottom,
    Armor,
    Cape,
}
public class TableDataManager : TableData
{
    public TableData GetData(int id)
    {
        return TableDataMap[id];
    }

    public List<TableData> GetDataByType()
    {
        //return TableDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
